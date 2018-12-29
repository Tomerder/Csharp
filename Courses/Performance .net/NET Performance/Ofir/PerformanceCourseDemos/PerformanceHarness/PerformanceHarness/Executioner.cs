using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PerformanceHarness
{
    public class Executioner
    {
        private Type _typeToExecute;
        private Stopwatch _stopper;
        private int[] _collectionCount = new int[GC.MaxGeneration + 1];
        private MethodInfo _setupMethod;
        private MethodInfo _teardownMethod;
        private MethodInfo _measureMethod;

        public Executioner(Type typeToExecute)
        {
            _typeToExecute = typeToExecute;

            FindMethods();
        }

        private void FindMethods()
        {
            foreach (MethodInfo method in
                _typeToExecute.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (Util.IsAttributeDefined<SetupMethodAttribute>(method))
                    _setupMethod = method;
                if (Util.IsAttributeDefined<TeardownMethodAttribute>(method))
                    _teardownMethod = method;
                if (Util.IsAttributeDefined<MeasurableMethodAttribute>(method))
                    _measureMethod = method;
            }
            if (_measureMethod == null)
                throw new ArgumentException("The type does not provide a method to measure");
        }

        public void Execute()
        {
            InitCollectionCount();
            if (_setupMethod != null)
                _setupMethod.Invoke(null, null);
            _stopper = Stopwatch.StartNew();
            _measureMethod.Invoke(null, null);
            _stopper.Stop();
            if (_teardownMethod != null)
                _teardownMethod.Invoke(null, null);
            StoreCollectionCount();
        }

        private void InitCollectionCount()
        {
            for (int i = 0; i < _collectionCount.Length; ++i)
                _collectionCount[i] = GC.CollectionCount(i);
        }

        private void StoreCollectionCount()
        {
            for (int i = 0; i < _collectionCount.Length; ++i)
                _collectionCount[i] = GC.CollectionCount(i) - _collectionCount[i];
        }

        public float ElapsedTicks
        {
            get { return _stopper.ElapsedTicks; }
        }

        public float ElapsedMilliseconds
        {
            get { return _stopper.ElapsedMilliseconds; }
        }

        public int GetCollectionCount()
        {
            int sum = 0;
            foreach (int i in _collectionCount) { sum += i; }
            return sum;
        }

        public int GetCollectionCount(int generation)
        {
            return _collectionCount[generation];
        }
    }
}
