using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using PerformanceHarness;

namespace PerformanceHarnessConsoleTester
{
    class ConsoleTester
    {
        static void Main(string[] args)
        {
            Orchestrator orchestrator = new Orchestrator();
            //orchestrator.CodeTemplateFile = "BasicTemplate.txt";
            //orchestrator.CodeStubReplacements = CodeGenerator.GenerateCodeStubReplacements("BasicTemplate_Sample.xml");
            orchestrator.CodeTemplateLocations.Add("BasicTemplate_GenericsDemo.txt");
            orchestrator.CodeTemplateLocations.Add("BasicTemplate_MultipleSample.txt");
            orchestrator.NumberOfInnerIterations = 100000;
            orchestrator.NumberOfOuterIterations = 10;
            orchestrator.OutputMethod = OutputMethod.Excel;
            orchestrator.Output = Console.Out;
            orchestrator.OutputFileName = @"C:\Temp\Results.xls";
            orchestrator.Orchestrate();
        }
    }
}
