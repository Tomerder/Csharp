
namespace RemoteExec.Shared
{
    /// <summary>
    /// Represents the execution result of a remote command.
    /// </summary>
    public enum RemoteExecutionResult
    {
        /// <summary>
        /// The command executed successfully.
        /// </summary>
        Success,
        /// <summary>
        /// The command is still executing.
        /// </summary>
        StillExecuting,
        /// <summary>
        /// The command's execution failed.
        /// </summary>
        Failure
    }

    /// <summary>
    /// The interface to the remote execution server.
    /// </summary>
    public interface IRemoteExec
    {
        /// <summary>
        /// Executes the specified command on the remote server and returns
        /// the result.  
        /// </summary>
        /// <param name="message">The message from the client.</param>
        /// <returns>The execution result.</returns>
        RemoteExecutionResult Execute(string message);
    }
}
