
namespace CodeContracts
{
    /// <summary>
    /// This demo demonstrates the use of Code Contracts to perform static
    /// analysis and runtime checking of preconditions and postconditions
    /// on a simple class that implements a stack using an array of limited
    /// capacity. Static analysis violations result in compilation warnings,
    /// while runtime checks that fail result in an exception.
    /// 
    /// Code Contracts must be enabled in the project settings in Visual
    /// Studio (under the Code Contracts tab) so that static analysis and
    /// runtime checking can be performed.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ArrayStack<string> stack = new ArrayStack<string>(1);
            stack.Push("Hello");
            stack.Push("World");
            //Unhandled Exception:
            //  System.Diagnostics.Contracts.__ContractsRuntime+ContractException:
            //  Precondition failed: !IsFull

            stack = new ArrayStack<string>(-1);
            //Compilation warning:
            //  CodeContracts: requires is false: capacity >= 0
        }
    }
}
