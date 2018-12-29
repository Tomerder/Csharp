using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSvn;
using System.Collections.ObjectModel;

namespace SvnLib
{
    public class SvnInterface
    {
		/*--------------------------------------------------------------------------*/
		
        public enum PathTypeEnum { WORKING_COPY_LOCAL_PATH, SVN_URL_PATH };

		public struct stLocksInfo
        {
            public stLocksInfo(string Owner, string FileFullPath, string Date)
            {
                _Owner = Owner;
                _FileFullPath = FileFullPath;
                _Date = Date;
            }
            public string _Owner;
            public string _FileFullPath;
            public string _Date;
        }
		
        /*--------------------------------------------------------------------------*/

        public bool Checkout(string _svnUrl, string _pathToCheckoutTo, out long _revisionCheckedOut)
        {
            _revisionCheckedOut = 0;

            //checkout
            try
            {
                //string cmd = SVN_CHECKOUT + _svnUrl + " " + _pathToCheckoutTo;
                //System.Diagnostics.Process.Start(SVN_EXE, cmd);

                SvnUpdateResult result;
                long revision;
                SvnCheckOutArgs args = new SvnCheckOutArgs();
            
                using (SvnClient client = new SvnClient())
                {
                    try
                    {                                      
                        SvnUriTarget SvnPath = new SvnUriTarget(_svnUrl);

                        //this is the where 'svn checkout' actually happens.
                        if (client.CheckOut(SvnPath, _pathToCheckoutTo, args, out result))
                        {
                            //BASE revisiom
                            //_revision = result.Revision;
                        }
                        else
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

        public bool GetLocBetweenPathes(string _pathToLocFrom, string _pathToLocTo, out Dictionary<string, string> _loc)
        {
            _loc = new Dictionary<string, string>();

            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnLogArgs svnLogArgs = new SvnLogArgs();
                    SvnTarget from = new SvnUriTarget(_pathToLocFrom, SvnRevision.Head);
                    SvnTarget to = new SvnUriTarget(_pathToLocTo, SvnRevision.Head);
                    System.Collections.ObjectModel.Collection<SvnDiffSummaryEventArgs> summary = new System.Collections.ObjectModel.Collection<SvnDiffSummaryEventArgs>();

                    //get loc from svn
                    client.GetDiffSummary(from, to, out summary);

                    //set output : Dictionary<string, string> _loc 
                    foreach (SvnDiffSummaryEventArgs change in summary)
                    {
                        string diffKind = "";
                        switch (change.DiffKind)
                        {
                            case(SvnDiffKind.Added):
                                diffKind = "Added";
                                break;
                            case(SvnDiffKind.Deleted):
                                diffKind = "Deleted";
                                break;
                            case(SvnDiffKind.Modified):
                                diffKind = "Modified";
                                break;
                            case(SvnDiffKind.Normal):
                                diffKind = "Normal";
                                break;
                        }

                        _loc[change.Path] = diffKind;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        public bool GetHistory(string _rootPath, bool _isDepthInfinity, bool _isOnlyFiles, out Dictionary<string, HistoryDataItem> _historyMap)
        {
            _historyMap = new Dictionary<string, HistoryDataItem>();

            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnLogArgs svnLogArgs = new SvnLogArgs();
                    System.Collections.ObjectModel.Collection<SvnDiffSummaryEventArgs> summary = new System.Collections.ObjectModel.Collection<SvnDiffSummaryEventArgs>();
                    //uri
                    SvnTarget svnPath = new SvnUriTarget(_rootPath, SvnRevision.Head);

                    //parameters
                    SvnListArgs listArgs = new SvnListArgs();
                    listArgs.Revision = SvnRevision.Head;
                    if (_isDepthInfinity)
                    {
                        listArgs.Depth = SvnDepth.Infinity; 
                    }
                    listArgs.RetrieveEntries = SvnDirEntryItems.AllFieldsV15;

                    //get loc from svn
                    Collection<SvnListEventArgs> outputList;
                    client.GetList(svnPath, listArgs, out outputList);

                    //fill output history map
                    FillHistoryMap(outputList, _isOnlyFiles, _historyMap);
                }
                catch(Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        private void FillHistoryMap(Collection<SvnListEventArgs> _svnHistoryDataList, bool _isOnlyFiles, Dictionary<string, HistoryDataItem> _historyMap)
        {
            foreach (SvnListEventArgs svnItem in _svnHistoryDataList)
            {
                if (!_isOnlyFiles || svnItem.Entry.NodeKind == SvnNodeKind.File)
                {
                    HistoryDataItem historyItem = new HistoryDataItem();
                    historyItem.ItemName = svnItem.Path;
                    historyItem.ItemRevision = svnItem.Entry.Revision;
                    historyItem.ItemSize = svnItem.Entry.FileSize;
                    historyItem.ItemTime = svnItem.Entry.Time;
                    historyItem.ChangedBy = svnItem.Entry.Author;

                    _historyMap[historyItem.ItemName] = historyItem;
                }
            }
        }

        /*--------------------------------------------------------------------------*/
		
		  //returns a List of stLocksInfo objects or null for failure
        // 1st input - SvnUrl, the svn path to look for locks
        // 2nd input - BreakLockUsersLst, list of users that their lock can be break (or null)
        public List<stLocksInfo> CollectLockers(string SvnUrl, List<string> BreakLockUsersLst)
        {
            List<stLocksInfo> LocksLst = null;
            SvnListArgs Args = new SvnListArgs();
            Args.Depth = SvnDepth.Infinity;
            Args.RetrieveLocks = true;

            SvnClient client = new SvnClient();

            Collection<SvnListEventArgs> col;

            try
            {
                client.GetList(new Uri(SvnUrl), Args, out col);

                LocksLst = new List<stLocksInfo>();
                foreach (SvnListEventArgs svnListEventArg in col)
                {

                    if (!(svnListEventArg.Lock == null))
                    {
                        string owner = svnListEventArg.Lock.Owner.ToString();

                        //Enforce "break lock" argument
                        SvnUnlockArgs ar = new SvnUnlockArgs();
                        ar.BreakLock = true;

                        if (!(BreakLockUsersLst == null))
                        {
                            //Break the lock in case locker name is equal to one of the input "BreakLockUsersLst" elements
                            if (BreakLockUsersLst.Any(owner.Contains))
                            {
                                client.RemoteUnlock(svnListEventArg.Uri, ar);
                            }
                        }

                        string Path = svnListEventArg.Path;
                        string date = svnListEventArg.Lock.CreationTime.ToShortDateString();

                        stLocksInfo LockInfo = new stLocksInfo(owner, Path, date);
                        LocksLst.Add(LockInfo);
                    }
                }
            }
            catch (Exception)
            {
                LocksLst = null;
            }

            return LocksLst;
        }

        /*--------------------------------------------------------------------------*/

        public bool Commit(string _pathToCommit, string _comment, out long _revisionCommited)
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

        public bool Tag(string _svnPathToMakeTagFrom, string _tagNameToCreate, string _comment, out long _revisionTagged)
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
                    SvnCommitResult result;

                    Uri svnPathToMakeTagFrom = new Uri(_svnPathToMakeTagFrom);
                    Uri svnTagToCreate = new Uri(_tagNameToCreate);
                    //TAG
                    success = client.RemoteCopy(svnPathToMakeTagFrom, svnTagToCreate, args, out result);
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


        /*--------------------------------------------------------------------------*/
    }
   
}
