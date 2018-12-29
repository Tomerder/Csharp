using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSvn;

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
                    success = client.Commit( _pathToCommit, args, out result);
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

        public bool Tag(string _svnPathToMakeTagFrom, string _tagNameToCreate,string _comment, out long _revisionTagged)
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
    }
   
}
