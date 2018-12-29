using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorInterfaces;
using System.Reflection;
using System.IO;

namespace PluggableCalculator
{
    public class TalksToPluginInSeparateAD : MarshalByRefObject
    {
        private Dictionary<string, ICalculatorPlugin> _plugins = new Dictionary<string, ICalculatorPlugin>();

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Init(string assembly)
        {
            Assembly asm = Assembly.Load(assembly);
            foreach (Type t in asm.GetExportedTypes())
            {
                if (typeof(ICalculatorPlugin).IsAssignableFrom(t) &&
                    t.IsDefined(typeof(CalculatorPluginAttribute), false))
                {
                    _plugins.Add(t.Name, asm.CreateInstance(t.FullName) as ICalculatorPlugin);
                }
            }
        }

        public IEnumerable<string> GetPluginOps()
        {
            return _plugins.Keys.ToArray();
        }

        public float Calculate(string type, float op1, float op2)
        {
            return _plugins[type].Calculate(op1, op2);
        }
    }

    public sealed class PluginLoader
    {
        private static Dictionary<string, TalksToPluginInSeparateAD> _plugins = new Dictionary<string, TalksToPluginInSeparateAD>();
        private static Dictionary<string, string> _opsToPlugins = new Dictionary<string, string>();

        public static IEnumerable<string> LoadPlugins()
        {
            foreach (string asmFile in Directory.GetFiles("Plugins", "*.dll"))
            {
                AppDomainSetup setup = new AppDomainSetup();
                setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                setup.PrivateBinPath = @"\Plugins";
                AppDomain domain = AppDomain.CreateDomain(
                    "PluginDomain_" + Path.GetFileNameWithoutExtension(asmFile),
                    AppDomain.CurrentDomain.Evidence,
                    setup);
                TalksToPluginInSeparateAD helper = (TalksToPluginInSeparateAD)domain.CreateInstanceAndUnwrap(
                    typeof(TalksToPluginInSeparateAD).Assembly.FullName,
                    typeof(TalksToPluginInSeparateAD).FullName);
                helper.Init(AssemblyName.GetAssemblyName(asmFile).FullName);
                _plugins.Add(asmFile, helper);

                foreach (string pluginOp in helper.GetPluginOps())
                {
                    _opsToPlugins.Add(pluginOp, asmFile);
                }
            }

            return _opsToPlugins.Keys.ToArray();
        }

        public static float Calculate(string op, float op1, float op2)
        {
            string asm = _opsToPlugins[op];
            return _plugins[asm].Calculate(op, op1, op2);
        }
    }
}
