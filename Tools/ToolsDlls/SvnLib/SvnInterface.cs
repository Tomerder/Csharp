using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSvn;
using System.IO;

namespace SvnLib
{
    public class SvnInterface
    {
        public enum PathTypeEnum { WORKING_COPY_LOCAL_PATH, SVN_URL_PATH };

        /*--------------------------------------------------------------------------*/

        public bool Checkout(string _svnUrl, string _pathToCheckoutTo, out long _revisionCheckedOut)
        {
            _revisionCheckedOut = 0;

            //checkout
            try
            {
                SvnUpdateResult result;
                long revision;
                SvnCheckOutArgs args = new SvnCheckOutArgs();

                using (SvnClient client = new SvnClient())
                {
                    try
                    {
                        SvnUriTarget SvnPath = new SvnUriTarget(_svnUrl);

                        //this is the where 'svn checkout' actually happens.
                        if (!client.CheckOut(SvnPath, _pathToCheckoutTo, args, out result))
                        {
                            return false;
                        }

                        //get base revision
                        long baseRevision;
                        bool success = GetRevision(_pathToCheckoutTo, PathTypeEnum.WORKING_COPY_LOCAL_PATH, 0, out baseRevision);
                        _revisionCheckedOut = baseRevision;
                    }
                    catch (SvnException se)
                    {
                        //in case of exception - try to checkout ***FILE***
                        //if(se.Message.Contains("refers to a file, not a directory"))
                        //{                       
                        //    try
                        //    {
                        //        Uri SvnPath = new Uri(_svnUrl);
                        //        string fileName = Path.GetFileName(SvnPath.LocalPath);
                        //        if (!String.IsNullOrEmpty(fileName))
                        //        {
                        //            using (var fileStream = File.Create(fileName))
                        //            {
                        //                client.Write(SvnTarget.FromUri(SvnPath), fileStream);
                        //            }
                        //        }
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        return false;
                        //    }      
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        return false;
                    }
                    catch (UriFormatException ufe)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Lock(string _pathToLock, string comment)
        {
            using (SvnClient client = new SvnClient())
            {
                SvnLockArgs svnLockArgs = new SvnLockArgs();

                try
                {
                    client.Lock(_pathToLock, comment);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Unlock(string _pathToLock)
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    client.Unlock(_pathToLock);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool GetRevision(string _pathToGetRevisionFrom, PathTypeEnum _pathType, int _revisionDepth, out long _revisionToGet)
        {
            _revisionToGet = 0;

            using (SvnClient client = new SvnClient())
            {
                try
                {
                    //get log
                    System.Collections.ObjectModel.Collection<SvnLogEventArgs> logEventArgs;
                    SvnLogArgs svnLogArgs = new SvnLogArgs();
                    svnLogArgs.Limit = _revisionDepth + 1;

                    if (_pathType == PathTypeEnum.WORKING_COPY_LOCAL_PATH)
                    {
                        client.GetLog(_pathToGetRevisionFrom, svnLogArgs, out logEventArgs);
                    }
                    else
                    {
                        Uri svnUrl = new Uri(_pathToGetRevisionFrom);
                        client.GetLog(svnUrl, svnLogArgs, out logEventArgs);
                    }

                    //get revision
                    _revisionToGet = logEventArgs[_revisionDepth].Revision;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Commit(string _pathToCommit, string _comment, out long _revisionCommited, bool _isCommitFile = false)
        {
            bool success = false;
            _revisionCommited = 0;

            //Commit
            using (SvnClient client = new SvnClient())
            {
                SvnCommitArgs args = new SvnCommitArgs();

                args.LogMessage = _comment;
                args.ThrowOnError = true;
                args.ThrowOnCancel = true;

                if (_isCommitFile)
                {
                    args.Depth = SvnDepth.Files;
                }

                try
                {
                    SvnCommitResult result;
                    success = client.Commit(_pathToCommit, args, out result);
                    if (result != null && success)
                    {
                        _revisionCommited = result.Revision;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Tag(string _pathToMakeTagFrom, string _tagNameToCreate, string _comment, out long _revisionTagged, bool _isTagFromWorkingCopy = false)
        {
            bool success = false;
            _revisionTagged = 0;

            //TAG
            using (SvnClient client = new SvnClient())
            {
                SvnCopyArgs args = new SvnCopyArgs();

                args.LogMessage = _comment;
                args.ThrowOnError = true;
                args.ThrowOnCancel = true;

                try
                {
                    SvnCommitResult result = null;
                  
                    Uri svnPathToMakeTagFrom = new Uri(_pathToMakeTagFrom);
                    Uri svnTagToCreate = new Uri(_tagNameToCreate);
                    //TAG
                    if (_isTagFromWorkingCopy)
                    {
                        success = client.RemoteCopy(_pathToMakeTagFrom, svnTagToCreate, args, out result);
                    }
                    else
                    {
                        success = client.RemoteCopy(svnPathToMakeTagFrom, svnTagToCreate, args, out result);
                    }                   
                    if (result != null && success)
                    {
                        _revisionTagged = result.Revision;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Import(string _pathToImport, string _tagNameToCreate, string _comment, out long _revisionTagged)
        {
            bool success = false;
            _revisionTagged = 0;

            //Import
            using (SvnClient client = new SvnClient())
            {
                SvnImportArgs args = new SvnImportArgs();

                args.LogMessage = _comment;
                args.ThrowOnError = true;
                args.ThrowOnCancel = true;

                try
                {
                    SvnCommitResult result;

                    Uri svnTagToCreate = new Uri(_tagNameToCreate);
                    //Import
                    success = client.Import(_pathToImport, svnTagToCreate, args, out result);
                    //success = client.RemoteImport(_pathToImport, svnTagToCreate, args, out result);
                    if (result != null && success)
                    {
                        _revisionTagged = result.Revision;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool Switch(string _localWorkingCopyPathToSwitch, string _svnPathToSwitchTo)
        {
            bool success = false;

            //Switch
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnUpdateResult result;
                    Uri svnTagToToSwitchTo = new Uri(_svnPathToSwitchTo);
                    //Switch
                    success = client.Switch(_localWorkingCopyPathToSwitch, svnTagToToSwitchTo, out result);
                }
                catch (Exception e)
                {
                    return false;
                }

                return success;
            }

        }

        /*--------------------------------------------------------------------------*/

        public bool SetExteranlPropertyByReplacing(string _localPathToChangeProperty, string _oldPathExteranlToReplace, string _newPathExteranl)
        {
            bool success;      

            try
            {
                using (SvnClient client = new SvnClient())
                {
                    //get (out) old property value 
                    string oldExternalValue;
                    success = client.GetProperty(_localPathToChangeProperty, "svn:externals", out oldExternalValue);
                    if (!success)
                    {
                        //failed getting current exteranl for working copy
                        return false;
                    }

                    bool isExternalAlreadyUpdated = oldExternalValue.Contains(_newPathExteranl);
                    if (success && !isExternalAlreadyUpdated)
                    {
                        //check legal - verify path to replace exist on current exteranl value
                        if (!oldExternalValue.Contains(_oldPathExteranlToReplace))
                        {
                            return false;
                        }

                        //create new exteranl value
                        string newExternalValue = oldExternalValue.Replace(_oldPathExteranlToReplace, _newPathExteranl);

                        success = client.SetProperty(_localPathToChangeProperty, "svn:externals", newExternalValue);
                        if (!success)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return success;       
        }

        /*--------------------------------------------------------------------------*/

        public bool SetLocalFolderProperty(string _localPathToChangeProperty, string _propertyToChange, string _newPropertyValue, out string _oldExternalValue)
        {
            bool success;
            _oldExternalValue = "";         

            try
            {
                using (SvnClient client = new SvnClient())
                {
                    //get (out) old property value 
                    success = client.GetProperty(_localPathToChangeProperty, _propertyToChange, out _oldExternalValue);
                    bool isExternalAlreadyUpdated = _oldExternalValue.Contains(_newPropertyValue);
                    if (success && !isExternalAlreadyUpdated)
                    {
                        success = client.SetProperty(_localPathToChangeProperty, _propertyToChange, _newPropertyValue);
                        if (!success)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return success;
        }

        /*--------------------------------------------------------------------------*/

        public bool GetSvnUrlForWorkingCopy(string _localPathToGetSvnUrlFor, out string _svnUrl)
        {
            _svnUrl = "";

            try
            {
                using (SvnClient client = new SvnClient())
                {
                    Uri uri = client.GetUriFromWorkingCopy(_localPathToGetSvnUrlFor);
                    if (uri == null)
                    {
                        return false;
                    }
                    else
                    {
                        _svnUrl = uri.ToString(); 
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true; ;
        }

        /*--------------------------------------------------------------------------*/

        public bool DeleteFolder(string _localPathToDelete)
        {
            bool success = false;

            try
            {
                using (SvnClient client = new SvnClient())
                {
                    success = client.Delete(_localPathToDelete);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return success; 
        }

        /*--------------------------------------------------------------------------*/

        public bool AddFolder(string _localPathToAdd)
        {
            bool success = false;

            try
            {
                using (SvnClient client = new SvnClient())
                {
                    success = client.Add(_localPathToAdd);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return success;
        }

        /*--------------------------------------------------------------------------*/
    }
}
