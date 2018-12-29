using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace PerformanceHarness
{
    public enum OutputMethod
    {
        CSV,
        XML,
        Excel
    }

    public class ProgressChangedEventArgs : EventArgs
    {
        private float _currentMeasurementPercentComplete;
        private float _totalMeasurementPercentComplete;

        public ProgressChangedEventArgs(float currentMeasurementPercentComplete,
                                        float totalMeasurementPercentComplete)
        {
            _currentMeasurementPercentComplete = currentMeasurementPercentComplete;
            _totalMeasurementPercentComplete = totalMeasurementPercentComplete;
        }

        public float CurrentMeasurementPercentComplete
        {
            get { return _currentMeasurementPercentComplete; }
        }

        public float TotalMeasurementPercentComplete
        {
            get { return _totalMeasurementPercentComplete; }
        }
    }
    
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);

    public class Orchestrator
    {
        private OutputMethod _outputMethod = OutputMethod.CSV;
        private IOutputProvider _outputProvider;
        private TextWriter _output;
        private string _outputFileName;
        private List<string> _additionalReferences = new List<string>();
        private List<string> _codeTemplateLocations = new List<string>();
        private List<string> _codeTemplateTexts = new List<string>();
        private Dictionary<string, string> _codeStubReplacements = new Dictionary<string, string>();
        private int _numberOfInnerIterations = 100000;
        private int _numberOfOuterIterations = 10;
        private CompileMode _compileMode;

        public event ProgressChangedEventHandler ProgressChanged;

        public IList<string> CodeTemplateLocations
        {
            get { return _codeTemplateLocations; }
        }

        public IList<string> CodeTemplateTexts
        {
            get { return _codeTemplateTexts; }
        }

        public IDictionary<string, string> CodeStubReplacements
        {
            get { return _codeStubReplacements; }
        }

        public int NumberOfInnerIterations
        {
            get { return _numberOfInnerIterations; }
            set { _numberOfInnerIterations = value; }
        }

        public int NumberOfOuterIterations
        {
            get { return _numberOfOuterIterations; }
            set { _numberOfOuterIterations = value; }
        }

        public OutputMethod OutputMethod
        {
            get { return _outputMethod; }
            set { _outputMethod = value; }
        }

        public TextWriter Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set { _outputFileName = value; }
        }

        public CompileMode CompileMode
        {
            get { return _compileMode; }
            set { _compileMode = value; }
        }

        public IList<string> AdditionalReferences
        {
            get { return _additionalReferences; }
        }

        public void Orchestrate()
        {
            switch(OutputMethod)
            {
                case OutputMethod.CSV:
                    _outputProvider = new CSVOutputProvider();
                    break;
                case OutputMethod.XML:
                    _outputProvider = new XMLOutputProvider();
                    break;
                case OutputMethod.Excel:
                    _outputProvider = new ExcelOutputProvider();
                    break;
                default:
                    throw new ArgumentException("_output provider for " + OutputMethod + "does not exist");
            }
            if (OutputMethod != OutputMethod.Excel)
            {
                if (Output == null)
                    Output = new StreamWriter(OutputFileName);
                _outputProvider.Output = Output;
            }
            _outputProvider.OutputFileName = OutputFileName;

            if (!CodeStubReplacements.ContainsKey("NUMBER_OF_ITERATIONS"))
                CodeStubReplacements.Add("NUMBER_OF_ITERATIONS", NumberOfInnerIterations.ToString());

            // Copy the references to the application directory.
            string applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (string referencedAssembly in _additionalReferences)
                File.Copy(referencedAssembly, Path.Combine(applicationDirectory, Path.GetFileName(referencedAssembly)));

            CodeGenerator generator = new CodeGenerator(CodeTemplateLocations, CodeTemplateTexts);
            generator.CompileMode = CompileMode;
            generator.Compile(CodeStubReplacements,
                              _additionalReferences.ConvertAll<string>(
                                delegate(string reference)
                                {   // Strip directory names, leave just the file names.
                                    return Path.GetFileName(reference);
                                }).ToArray());

            Assembly generatedAssembly = Assembly.LoadFile(generator.OutputAssemblyLocation);

            List<Type> measurableClasses = GetMeasurableClassesForAssembly(generatedAssembly);
            _outputProvider.WriteHeader();
            //WriteDummyHeaders(measurableClasses);
            for (int i = 0; i < measurableClasses.Count; ++i)
            {
                Executioner executioner = new Executioner(measurableClasses[i]);
                for (int j = 0; j < NumberOfOuterIterations; ++j)
                {
                    executioner.Execute();
                    _outputProvider.WriteEntry("Time-" + measurableClasses[i].FullName,
                                               executioner.ElapsedTicks / NumberOfInnerIterations);
                    //_outputProvider.WriteEntry("GC-" + measurableClasses[i].FullName,
                    //                           executioner.GetCollectionCount());

                    if (ProgressChanged != null)
                    {
                        float currentMeasurementComplete = ((float)j) / NumberOfOuterIterations;
                        float totalMeasurementComplete = ((float)i) / measurableClasses.Count;
                        totalMeasurementComplete += (currentMeasurementComplete) / measurableClasses.Count;
                        ProgressChanged(this,
                                        new ProgressChangedEventArgs(currentMeasurementComplete * 100,
                                                                     totalMeasurementComplete * 100));
                    }
                }
            }
            _outputProvider.Finish();

            if (Output != null)
            {
                Output.Close();
                Output = null;
            }
        }

        private void WriteDummyHeaders(List<Type> measurableClasses)
        {
            foreach (Type mc in measurableClasses)
                _outputProvider.WriteEntry("Time-" + mc.FullName, 0);
            foreach (Type mc in measurableClasses)
                _outputProvider.WriteEntry("GC-" + mc.FullName, 0);
        }

        private static List<Type> GetMeasurableClassesForAssembly(Assembly generatedAssembly)
        {
            List<Type> measurableClasses = new List<Type>();
            Array.ForEach(generatedAssembly.GetTypes(),
                          delegate(Type t)
                          {
                              if (Util.IsAttributeDefined<MeasurableClassAttribute>(t))
                                  measurableClasses.Add(t);
                          });
            return measurableClasses;
        }
    }
}
