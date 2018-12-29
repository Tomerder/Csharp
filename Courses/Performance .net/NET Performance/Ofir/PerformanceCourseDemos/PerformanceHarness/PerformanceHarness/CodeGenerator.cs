using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.CSharp;

namespace PerformanceHarness
{
    public enum CompileMode
    {
        Release,
        Debug
    }

    public class CodeGenerationException : Exception
    {
        private CompilerErrorCollection _errors;

        public CodeGenerationException(string errorMessage, CompilerErrorCollection errors)
            : base(errorMessage)
        {
            _errors = errors;
        }

        public string ErrorMessage
        {
            get { return this.Message; }
        }

        public CompilerErrorCollection CompilerErrors
        {
            get { return _errors; }
        }
    }

    public class CodeGenerator
    {
        private List<string> _codeStubs = new List<string>();
        private List<string> _codeTemplateTexts = new List<string>();
        private string _outputAssemblyLocation;
        private CompileMode _compileMode = CompileMode.Release;
        private static readonly string _baseOutputDirectory = @"C:\Temp\PerformanceHarness";
        private static readonly Regex _stubRegex = new Regex(@"%(?<stub>\w+)%");

        public CodeGenerator(IEnumerable<string> codeTemplateLocations, IEnumerable<string> codeTemplateTexts)
            : this()
        {
            if (codeTemplateLocations != null)
                AddCodeTemplateLocations(codeTemplateLocations);
            if (codeTemplateTexts != null)
                AddCodeTemplateTexts(codeTemplateTexts);
        }

        public CodeGenerator()
        {
            OutputAssemblyLocation =
                Path.ChangeExtension(Path.Combine(_baseOutputDirectory, Guid.NewGuid().ToString()), ".dll");            
        }

        public void AddCodeTemplateLocations(IEnumerable<string> codeTemplateLocations)
        {
            foreach (string codeTemplateLocation in codeTemplateLocations)
                _codeTemplateTexts.Add(File.ReadAllText(codeTemplateLocation));
        }

        public void AddCodeTemplateTexts(IEnumerable<string> codeTemplateTexts)
        {
            _codeTemplateTexts.AddRange(codeTemplateTexts);
        }

        private void GetCodeStubsFromTemplates()
        {
            foreach (string codeTemplate in _codeTemplateTexts)
            {
                foreach (Match match in _stubRegex.Matches(codeTemplate))
                {
                    string codeStub = match.Groups["stub"].Captures[0].Value;
                    if (!_codeStubs.Contains(codeStub))
                        _codeStubs.Add(codeStub);
                }
            }
        }

        public string OutputAssemblyLocation
        {
            get { return _outputAssemblyLocation; }
            set { _outputAssemblyLocation = value; }
        }

        public CompileMode CompileMode
        {
            get { return _compileMode; }
            set { _compileMode = value; }
        }

        public static IDictionary<string, string> GenerateCodeStubReplacements(XmlDocument xmlDocument)
        {
            IDictionary<string, string> codeStubReplacements = new Dictionary<string, string>();
            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                codeStubReplacements.Add(node.Name, node.InnerText);
            }
            return codeStubReplacements;
        }

        public static IDictionary<string, string> GenerateCodeStubReplacements(string xmlDocumentFileName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlDocumentFileName);
            return GenerateCodeStubReplacements(document);
        }

        public void Compile(IDictionary<string, string> codeStubReplacements, params string[] references)
        {
            string[] sourcesToCompile = GetSourcesToCompile(codeStubReplacements);
            
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = false;
            parameters.GenerateExecutable = false;
            if (CompileMode == CompileMode.Release)
                parameters.CompilerOptions += " /optimize";
            if (CompileMode == CompileMode.Debug)
                parameters.CompilerOptions += " /debug";
            parameters.CompilerOptions += " /unsafe";
            parameters.OutputAssembly = OutputAssemblyLocation;
            parameters.ReferencedAssemblies.Add("PerformanceHarness.dll");
            if (references != null && references.Length > 0)
                parameters.ReferencedAssemblies.AddRange(references);

            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, sourcesToCompile);
            if (results.Errors.Count > 0)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                {
                    errorMessage.AppendFormat("Line: {0} Error: {1} '{2}'", error.Line, error.ErrorNumber,
                                              error.ErrorText);
                }
                throw new CodeGenerationException(errorMessage.ToString(), results.Errors);
            }
        }

        private string[] GetSourcesToCompile(IDictionary<string, string> codeStubReplacements)
        {
            GetCodeStubsFromTemplates();

            string[] sources = new string[_codeTemplateTexts.Count];
            for (int i = 0; i < _codeTemplateTexts.Count; ++i)
            {
                StringBuilder source = new StringBuilder(_codeTemplateTexts[i]);
                foreach (string codeStub in codeStubReplacements.Keys)
                {
                    if (!_codeStubs.Contains(codeStub))
                        throw new ArgumentException("Code stub " + codeStub + " does not exist in the template");

                    source.Replace("%" + codeStub + "%", codeStubReplacements[codeStub]);
                }
                sources[i] = source.ToString();
            }

            return sources;
        }
    }
}
