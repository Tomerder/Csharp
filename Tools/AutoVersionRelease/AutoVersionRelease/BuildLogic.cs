using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UtilsLib;
using System.Windows;

namespace AutoVersionRelease
{
    static class BuildLogic
    {
        /*----------------------------------------------------------------------------*/

        private const string VERSIONS_LIB_CONFIG_FILE_PATH = "C:/AutoRelease/VersionsLibConfigFile.xml";

        /*----------------------------------------------------------------------------*/

        static public bool DoBuildLogic(Dictionary<string, Csci> _cscisMap, MainWindow _mainWindow)
        {
            bool success = true;
            string message = "";

            //init version lib
            success = InitVersionLib();
            if (!success)
            {
                message = "Error initialize versions lib";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
            }

            //build each CSCI
            foreach (Csci csci in _cscisMap.Values)
            {
                message = " ******************* " + csci.CsciName + " ******************* ";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);

                //DO CSCI
                Csci.WorkingCopyStatusEnum workingCopyStatus;
                success = BuildLogic.DoCsci(csci, _cscisMap, _mainWindow, out workingCopyStatus);
                if (!success)
                {
                    //_mainWindow.AppendMessage(csci.CsciName + " : Error DoCSCI", MainWindow.MessageType.ERROR);
                    return false;
                }
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        //recursive - for dependencies                                                      //in order that CSCI should be built because dependent was built (e.g : OFP_SR1 <- COMMON_SR1) : will turn on isBuiltCsciDueToDependentCsci flag
        static public bool DoCsci(Csci _csci, Dictionary<string, Csci> _cscisMapForDependencies, MainWindow _mainWindow, out Csci.WorkingCopyStatusEnum _workingCopyStatus)
        {
            bool success = true;
            string message = "";

            //set reurn parameter
            _workingCopyStatus = _csci.WorkingCopyStatus;

            message = _csci.CsciName + " : Do CSCI...";
            UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);

            //check recursion stop condition
            bool isStopCondition = DoCsciIsStopCondition(_csci, _mainWindow);
            if (isStopCondition)
            {
                return true;
            }

            //run recursion for dependencies
            bool isBuiltCsciDueToDependentCsci = false;
            success = DoCsciRecDependencies(_csci, _cscisMapForDependencies, _mainWindow, out isBuiltCsciDueToDependentCsci);
            if (!success)
            {
                return false;
            }

            //get working copy VERSION
            string versionWorkingCopy;
            bool successGettingVersion = VersionsLibInterfaceLayer.GetCsciVersion(_csci.CsciName, out versionWorkingCopy);
            if (!successGettingVersion)
            {
                message = _csci.CsciName + " : Error reading CSCI version";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                return false;
            }
            //set version to CSCI
            _csci.WorkingCopyVersion = versionWorkingCopy;

            //check if build is required : any changes from last TAG, (or dependent CSCI was changed - TODO)
            bool isEqualToTag;
            bool successCompareRevisions = SvnLogic.IsWorkingCopyEqualToTag(_csci, out isEqualToTag);
            if (!successCompareRevisions)
            {
                message = _csci.CsciName + " : Error comparing working copy revision to last TAG";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                return false;
            }

            //in case of SYSINT ask if build anyway
            bool isSysintBuiltAnyway = false;
            if (_csci.IsSysint && isEqualToTag && !isBuiltCsciDueToDependentCsci)
            {
                //ask if Build CSCI anyway - for cases in which every included CSCIs were configured to TAG
                string msgText = _csci.CsciName + " : no changes on SYSINT or on dependent CSCIs - Build anyway ?";
                string title = "BUILD SYSINT ?";
                isSysintBuiltAnyway = UtilsGui.MessageBoxYesNo(title, msgText);
            }

            //build csci if build is required
            if (!isEqualToTag || isBuiltCsciDueToDependentCsci || isSysintBuiltAnyway)
            {
                if (!isEqualToTag)
                {
                    message = _csci.CsciName + " : Working copy revision is NOT Equal To Tag " + _csci.WorkingCopyVersion + " - CSCI should be built";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                }

                if (isBuiltCsciDueToDependentCsci)
                {
                    message = _csci.CsciName + " : Dependent CSCI have Changed - CSCI should be built";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                }                   

                success = DoCsciBuildProcess(_csci, _mainWindow);               
                if (success)
                {
                    //set working copy status 
                    _csci.WorkingCopyStatus = Csci.WorkingCopyStatusEnum.BUILT_AND_READY;
                    message = _csci.CsciName + " : Working copy is Built and Ready";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                }
                else
                {
                    message = _csci.CsciName + " : Build failed";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }              
            }
            else
            {
                //set working copy status 
                _csci.WorkingCopyStatus = Csci.WorkingCopyStatusEnum.WORKING_COPY_REV_AND_DEPENDENTS_ARE_EQUAL_TO_TAGS;
                message = _csci.CsciName + " : Working copy revision (and dependent cscis revisions) is equal to TAG " + _csci.WorkingCopyVersion + " - No action required";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
            }

            //if sysint - zip products (BIN folder)
            if (_csci.IsSysint)
            {
                string binPathToZip = _csci.LocalPathToCheckout + "/Bin";
                string luhPathToZip = _csci.LocalPathToCheckout + "/LUH";
                string zipFileName = "OUT_" + _csci.CsciName;
                string outputPath = GeneralConfigs.Instance.OutputsPath;
                string zipExeLocation = GeneralConfigs.Instance.InputsPath;
                bool successBin = UtilsLib.UtilsLib.ZipPath(zipExeLocation, binPathToZip, outputPath, zipFileName);
                bool successLuh = UtilsLib.UtilsLib.ZipPath(zipExeLocation, luhPathToZip, outputPath, zipFileName);
                if (successBin && successLuh)
                {
                    message = _csci.CsciName + " : Products are ready - " + outputPath + "/" + zipFileName + ".zip";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                }
                else
                {
                    message = _csci.CsciName + " : zip Products was failed";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                }
            }

            //set reurn parameter
            _workingCopyStatus = _csci.WorkingCopyStatus;

            return true;
        }

        /*----------------------------------------------------------------------------*/

        static private bool DoCsciBuildProcess(Csci _csci, MainWindow _mainWindow)
        {
            bool success = true;
            string message = "";

            //execute PRE build script
            if (_csci.PreBuildScriptPath.Length > 0)
            {
                message = _csci.CsciName + " : Execute pre build script : " + _csci.PreBuildScriptPath + " - Please wait...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                success = ExecutePreBuildScript(_csci.PreBuildScriptPath, true);
                if (success)
                {
                    message = _csci.CsciName + " : Pre build script executed successfuly";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                }
                else
                {
                    message = _csci.CsciName + " : Pre build script execution ERROR";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }
            }

            //increase version number
            string versionIncreased;
            success = VersionsLibInterfaceLayer.IncreaseCsciVersion(_csci.CsciName, out versionIncreased);
            if (success)
            {
                message = _csci.CsciName + " : Version was increased to : " + versionIncreased;
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                _csci.WorkingCopyVersionAfterIncrease = versionIncreased;
            }
            else
            {
                message = _csci.CsciName + " : Version increased Failed";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                return false;
            }

            //if SYSINT -> align LUH versions file
            if (_csci.IsSysint)
            {
                success = VersionsLibInterfaceLayer.AlignLuhVersionsFile(_csci.CsciName);
                if (success)
                {
                    message = _csci.CsciName + " : LUH Version file was aligned";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                    _csci.WorkingCopyVersionAfterIncrease = versionIncreased;
                }
                else
                {
                    message = _csci.CsciName + " : LUH Version file align was Failed";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }
            }

            //build CSCI
            if (_csci.BuildScriptPath.Length > 0)
            {
                message = _csci.CsciName + " : Build started...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                bool isTryAgainIfFailed = true;
                
                success = BuildCsci(_csci.BuildScriptPath, isTryAgainIfFailed);
                if (success)
                {
                    message = _csci.CsciName + " : Build successful";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                }
                else
                {
                    message = _csci.CsciName + " : Build ERROR";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }
            }

            //CSCI is ready for commit and tag
            message = _csci.CsciName + " : Is ready for commit and create TAG - " + _csci.WorkingCopyVersionAfterIncrease;
            UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.HIGHLIGHT);

            if (_mainWindow.IsSelectBoxChecked(CheckBoxType.AUTO_COMMIT))
            {
                //COMMIT to SVN
                long revisionCommited;
                bool successCommit = SvnLogic.CommitCsci(_csci, _mainWindow, out revisionCommited);
                if (!successCommit)
                {
                    message = _csci.CsciName + " : was not Committed, please make sure to COMMIT changes and create a TAG manually";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.HIGHLIGHT);
                }
                else               
                {
                    //TAG CSCI
                    long revisionTagged;
                    bool successTag = SvnLogic.TagCsci(_csci, _mainWindow, out revisionTagged);
                    if (!successTag)
                    {
                        message = _csci.CsciName + " : was not Tagged, please make sure to create TAG manually : " + _csci.WorkingCopyVersionAfterIncrease;
                        UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.HIGHLIGHT);
                    }
                }
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        private static bool BuildCsci(string _BuildScriptPath, bool _isAskToTryAgainIfFailed = false)
        {
            bool success = false;
            bool isTryAgainFromUser = false;

            do
            {
                success = UtilsLib.UtilsLib.ExecuteCommand(_BuildScriptPath);

                if (!success && _isAskToTryAgainIfFailed)
                {
                    //ask if try again
                    string msgText = "Built failed - Try again ?";
                    string title = "BUILD FAILED";
                    isTryAgainFromUser = UtilsGui.MessageBoxYesNo(title, msgText);
                }
            } while (!success && _isAskToTryAgainIfFailed && isTryAgainFromUser);

            return success;
        }

        /*----------------------------------------------------------------------------*/

        private static bool ExecutePreBuildScript(string _preBuildScriptPath, bool _isAskToTryAgainIfFailed = false)
        {
            bool success = true;
            bool isTryAgainFromUser = false;

            try
            {
                int indexToCut = _preBuildScriptPath.LastIndexOf("\\") + 1;

                string cmd = _preBuildScriptPath;
                string path = _preBuildScriptPath.Substring(0, indexToCut);                

                do
                {
                    success = UtilsLib.UtilsLib.ExecuteCommand(cmd, "", path);

                    if (!success && _isAskToTryAgainIfFailed)
                    {
                        //ask if try again
                        string msgText = "Pre build script failed - Try again ?";
                        string title = "PRE BUILD FAILED";
                        isTryAgainFromUser = UtilsGui.MessageBoxYesNo(title, msgText);
                    }
                } while (!success && _isAskToTryAgainIfFailed && isTryAgainFromUser);

            }
            catch
            {
                success = false;
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static private bool DoCsciRecDependencies(Csci _csci, Dictionary<string, Csci> _cscisMapForDependencies, MainWindow _mainWindow, out bool _isBuiltCsciDueToDependentCsci)
        {
            string message = "";

            //set return parameter
            _isBuiltCsciDueToDependentCsci = false;

            if (_csci.DependentCsciList.Count != 0)
            {
                message = _csci.CsciName + " : Check dependencies...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
            }

            foreach (string csciName in _csci.DependentCsciList)
            {
                Csci dependentCsci;
                try
                {
                    dependentCsci = _cscisMapForDependencies[csciName];
                }
                catch
                {
                    message = _csci.CsciName + " : Error find dependent CSCI - " + csciName;
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }

                //recursive
                Csci.WorkingCopyStatusEnum workingCopyStatus;
                bool success = DoCsci(dependentCsci, _cscisMapForDependencies, _mainWindow, out workingCopyStatus);
                if (!success)
                {
                    message = _csci.CsciName + " : Error on dependent CSCI - " + csciName;
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }

                //if one of dependencies was built - CSCI should be built 
                if (workingCopyStatus == Csci.WorkingCopyStatusEnum.BUILT_AND_READY)  /* dependentCsci affects csci products */
                {
                    _isBuiltCsciDueToDependentCsci = true;
                }
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/

        static private bool DoCsciIsStopCondition(Csci _csci, MainWindow _mainWindow)
        {
            string message = "";

            //if configured to TAG - no action neccessary
            if (_csci.SvnPathType == Csci.SvnPathTypeEnum.TAG)
            {
                message = _csci.CsciName + " : is set to TAG - No action required";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);         
                return true;
            }

            //if already built and ready - no action neccessary
            if (_csci.WorkingCopyStatus == Csci.WorkingCopyStatusEnum.BUILT_AND_READY)
            {
                message = _csci.CsciName + " : is already built and ready - No action required";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE); 
                return true;
            }

            //if working copy is the same revision as TAG - no action neccessary
            if (_csci.WorkingCopyStatus == Csci.WorkingCopyStatusEnum.WORKING_COPY_REV_AND_DEPENDENTS_ARE_EQUAL_TO_TAGS)
            {
                message = _csci.CsciName + " :  Working copy revision (and dependent cscis revisions) are equal to TAG - No action required";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE); 
                return true;
            }

            //if not configured to TRUNK/BRANCH/TAG (like TOOLS)
            if (_csci.SvnPathType == Csci.SvnPathTypeEnum.NONE)
            {
                message = _csci.CsciName + " : SVN Path is configured to NONE - No action required";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE); 
                return true;
            }

            return false;
        }

        /*----------------------------------------------------------------------------*/

        static public bool MapEnviriement(MainWindow _mainWindow)
        {
            bool success = true;
            string message = "";
            string mappingScriptPath = GeneralConfigs.Instance.MappingScriptPath;

            //MAP enveirement
            bool isTryAgain = true;
            do
            {               
                success = UtilsLib.UtilsLib.ExecuteCommand(mappingScriptPath);
                if (!success)
                {
                    //ask if try again
                    string msgText = "Mapping failed - Try again ?";
                    string title = "MAPPING FAILED";
                    isTryAgain = UtilsGui.MessageBoxYesNo(title, msgText);
                }
            } while (!success && isTryAgain);

            if (success)
            {
                message = "enveirement mapped successfuly";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS); 
            }
            else
            {
                message = "Error map enveirement";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR); 
                return false;
            }
          
            return success;
        }

        /*----------------------------------------------------------------------------*/

        static public bool InitVersionLib()
        {
            //create VersionsLibInterfaceLayer for later useage
            bool success = VersionsLibInterfaceLayer.CreateVersionsLibInterfaceLayer(VERSIONS_LIB_CONFIG_FILE_PATH);

            return success;
        }

        /*----------------------------------------------------------------------------*/    
    }
}
