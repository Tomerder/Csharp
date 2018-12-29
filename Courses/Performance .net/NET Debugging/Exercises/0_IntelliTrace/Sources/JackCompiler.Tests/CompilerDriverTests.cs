using JackCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JackCompiler.Tests
{
    [TestClass]
    public class CompilerDriverTests
    {
        [TestMethod]
        public void ExecuteTest()
        {
            CompilerDriver.Execute();
        }
    }
}
