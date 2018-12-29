using System;
using System.Runtime.Remoting;
using RemoteExec.Shared;

namespace RemoteExec.Client
{
    /// <summary>
    /// Connects to the remote execution server 
    /// </summary>
    class RemoteExecClient
    {
        static void Main(string[] args)
        {
            //Demonstrates synchronous execution.  The ExecuteSync command does not return
            //until execution completes.
            IRemoteExec rexec = (IRemoteExec)RemotingServices.Connect(typeof(IRemoteExec), "tcp://localhost:9090/RemoteExec.rem");
            RemoteExecutionResult result = rexec.Execute("hello");
            Console.WriteLine("Execution result: " + result);
            Console.ReadKey();
        }
    }
}
