using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JackCompiler
{
    public sealed class CompilerDriver
    {
        private static CodeGeneratorOptions _options = new CodeGeneratorOptions
                                                           {
                                                               Output = Console.Out
                                                           };

        public static void Execute()
        {
            string testProgramDir =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ICodeGenerator codeGenerator = new CCodeGenerator();
            _options.Output = File.CreateText("out.c");
            codeGenerator.SetOptions(_options);
            codeGenerator.EmitEnvironment();
            Tokenizer tokenizer = new Tokenizer(
                File.OpenText(Path.Combine(testProgramDir, "TestProgram.jack")));
            Parser parser = new Parser(tokenizer, codeGenerator);
            parser.Parse();
            codeGenerator.EmitBootstrapper();
            _options.Output.Close();
        }
    }
}
