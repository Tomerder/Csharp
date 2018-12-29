using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SvnLib;
using System.IO;
using UtilsLib;

namespace AutoVersionRelease
{
    static class SvnLogic
    {
        /*----------------------------------------------------------------------------*/

        static public bool CheckoutAllCscis(Dictionary<string, Csci> _cscisMap, MainWindow _mainWindow)
        {
            bool success = true;
            string message = "";

            message = "Checkout environment.....";
            UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);

            foreach (Csci csci in _cscisMap.Values)
            {
                //delete path to checkout to - if exists
                if (Directory.Exists(csci.LocalPathToCheckout))
                {
                    //if defined that no checkout is needed in case CSCI working copy (local folder) already exists -> finish
                    if (!csci.IsCheckoutIfAlreadyExists)
                    {
                        message = csci.CsciName + " : Working copy already exists - no checkout needed (by config file definition)";
                        UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                        //continue to next CSCI
                        continue;
                    }
                 
                    //DELETE folder - in case delete failed ask if try again
                    bool isTryAgainMap = false;

                    do
                    {
                        success = UtilsLib.UtilsLib.DeleteFolderAndContent(csci.LocalPathToCheckout);
                        
                        if (!success)
                        {
                            //ask if try again
                            string msgText = csci.LocalPathToCheckout + " : Delete failed - Try again ?";
                            string title = "DELETE FAILED";
                            isTryAgainMap = UtilsGui.MessageBoxYesNo(title, msgText);
                        }                       
                    } while (!success && isTryAgainMap);

                    if (success)
                    {
                        message = csci.CsciName + " : Local folder Deleted successfuly";
                        UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                    }
                    else
                    {
                        message = csci.CsciName + " : Error Deleting Local folder";
                        UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                        return false;
                    }
                }

                //CHECKOUT
                message = csci.CsciName + " : Checkout " + csci.SvnUrlPath + " - Please wait...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);
                SvnInterface m_svn = new SvnInterface();
                long revisionCheckout;               
                bool isTryAgainCheckout = true;
                do
                {
                    success = m_svn.Checkout(csci.SvnUrlPath, csci.LocalPathToCheckout, out revisionCheckout);

                    if (!success)
                    {
                        //ask if try again
                        string msgText = csci.CsciName + " : checkout failed - Try again ?";
                        string title = "CHECKOUT FAILED";
                        isTryAgainCheckout = UtilsGui.MessageBoxYesNo(title, msgText);
                    }
                } while (!success && isTryAgainCheckout);
             
                if(success)
                {
                    //set working copy revision
                    csci.RevisionCheckedOut = revisionCheckout;
                    //set working copy status 
                    csci.WorkingCopyStatus = Csci.WorkingCopyStatusEnum.CHECKED_OUT;
                }
                else
                {
                    message = csci.CsciName + " : Error Checkout";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                    return false;
                }

                message = csci.CsciName + " : Checkout successfuly - Revision : " + revisionCheckout.ToString();
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);           
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/
      
        static public bool IsWorkingCopyEqualToTag(Csci _csci, out bool _isEqual)
        {
            _isEqual = false; ;

            try
            {
                //get TAG svn url - for version from .h file (/tags/<version>)
                string tagPathToCompare;
                if (!CreateSvnTagFromTrunk(_csci.SvnUrlPath, _csci.WorkingCopyVersion, out tagPathToCompare))
                {
                    return false;
                }
                
                //get revision from TAG
                SvnInterface m_svn = new SvnInterface();
                long revisionFromTag;
                bool success = m_svn.GetRevision(tagPathToCompare, SvnInterface.PathTypeEnum.SVN_URL_PATH, 1, out revisionFromTag);
                if (!success)
                {
                    return false;
                }

                //get revision of working copy - checked out path from SVN
                string svnWorkingCopyUrl = _csci.SvnUrlPath;
                long revisionFromTrunk;
                success = m_svn.GetRevision(svnWorkingCopyUrl, SvnInterface.PathTypeEnum.SVN_URL_PATH, 0, out revisionFromTrunk);
                if (!success)
                {
                    return false;
                }

                // is revision of working copy == revision of TAG 
                //_isEqual = (_csci.RevisionCheckedOut == revisionFromTag);
                _isEqual = (revisionFromTrunk == revisionFromTag);
                
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/

        static public bool CommitCsci(Csci _csci, MainWindow _mainWindow, out long _revision)
        {
            bool success = false;
            string message = "";
            _revision = 0;
         
            //display dialog message for commit confirmation
            string title = "COMMIT";
            string dialogMessage = _csci.CsciName + " : Do you want to commit changes at " + _csci.LocalPathToCheckout + " ?";
            dialogMessage += "\n(Please make sure all the changes should be commited, also make sure to perform SVN add/delete before commiting if nessasary)";
            dialogMessage += "\nPlease enter log message and press OK for COMMIT";
            dialogMessage += "\nOtherwise Please press CANCEL"; 

            //commit default message
            string defaultCommitLogMessage = _csci.CsciName + " : update version " + _csci.WorkingCopyVersionAfterIncrease;
            if (_csci.IsSysint)
            {
                defaultCommitLogMessage += " + LUH versions";
            }
            else if (_csci.PreBuildScriptPath != "")
            {
                defaultCommitLogMessage += " + Generated code";
            }
            defaultCommitLogMessage += " + Products : For release " + UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxVersionToRelease);

            //display DIALOG BOX   
            string logMessageToCommit = UtilsGui.DialogBox(title, dialogMessage, defaultCommitLogMessage);
          
            //in case user pressed CANCEL : logMessageToCommit will be ""
            if (logMessageToCommit.Length > 0)
            {
                message = _csci.CsciName + " : COMMIT is being performed, please wait...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);

                //COMMIT
                SvnInterface m_svn = new SvnInterface();
                success = m_svn.Commit(_csci.LocalPathToCheckout, logMessageToCommit, out _revision);
                if (success)
                {
                    message = _csci.CsciName + " : Commited successfully : revision " + _revision;
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                }
                else
                {
                    message = _csci.CsciName + " : Commit failed";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                }
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static public bool TagCsci(Csci _csci, MainWindow _mainWindow, out long _revision)
        {
            bool success = false;
            string message = "";
            _revision = 0;

            //TAG to create
            string tagToCreate;
            if (!CreateSvnTagFromTrunk(_csci.SvnUrlPath, _csci.WorkingCopyVersionAfterIncrease, out tagToCreate))
            {
                message = _csci.CsciName + " : failed to assamble TAG path";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                return false;
            }

            //display dialog message for commit confirmation
            string title = "TAG";
            string dialogMessage = "Do you want to create TAG for : " + _csci.CsciName + " ? ";
            dialogMessage += "\n(TAG will be created from " + _csci.SvnUrlPath + " ,please make sure that all changes are commited)";
            dialogMessage += "\nPlease enter log message and press OK for creating TAG " + tagToCreate;
            dialogMessage += "\nOtherwise Please press CANCEL";

            //commit default message
            string defaultTagLogMessage = _csci.CsciName + " : TAG version " + _csci.WorkingCopyVersionAfterIncrease;
            defaultTagLogMessage += " : For release " + UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxVersionToRelease);

            //display DIALOG BOX   
            string logMessageToTag = UtilsGui.DialogBox(title, dialogMessage, defaultTagLogMessage);

            //in case user pressed CANCEL : logMessageToTag will be ""
            if (logMessageToTag.Length > 0)
            {
                message = _csci.CsciName + " : TAG is being created, please wait...";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.NONE);

                //TAG
                SvnInterface m_svn = new SvnInterface();
                success = m_svn.Tag(_csci.SvnUrlPath, tagToCreate,logMessageToTag, out _revision);
                if (success)
                {
                    message = _csci.CsciName + " : TAG " + tagToCreate  + " was created successfully : revision " + _revision;
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
                }
                else
                {
                    message = _csci.CsciName + " : TAG failed";
                    UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR);
                }
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static private bool CreateSvnTagFromTrunk(string _svnUrlTrunk, string _versionOfTag, out string _svnUrlTag)
        {
            _svnUrlTag = "";

            string toReplaceWith = "tags/" + _versionOfTag;
            //repalce trunk
            string toReplace = "trunk";
            try
            {
                _svnUrlTag = _svnUrlTrunk.Replace(toReplace, toReplaceWith);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/
    }
}
