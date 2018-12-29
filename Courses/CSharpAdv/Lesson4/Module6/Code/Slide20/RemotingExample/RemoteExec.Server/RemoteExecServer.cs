using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemoteExec.Shared;

namespace RemoteExec.Server
{
    /// <summary>
    /// A .NET Remoting server that implements the IRemoteExec interface
    /// </summary>
    class RemoteExecServer : MarshalByRefObject, IRemoteExec
    {
        /// <summary>
        /// Registers a remoting channel and begins listening on port 9090
        /// for remote execution requests.
        /// </summary>
        static void Main(string[] args)
        {
            ChannelServices.RegisterChannel(new TcpChannel(9090), false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteExecServer), 
                "RemoteExec.rem", 
                WellKnownObjectMode.Singleton);

            Console.WriteLine("Server is listening, press RETURN to terminate.");
            Console.ReadLine();
        }

        public RemoteExecutionResult Execute(string message)
        {
            try
            {
                Console.WriteLine("got " + message);
                return RemoteExecutionResult.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e);
                return RemoteExecutionResult.Failure;
            }
        }
    }
}
