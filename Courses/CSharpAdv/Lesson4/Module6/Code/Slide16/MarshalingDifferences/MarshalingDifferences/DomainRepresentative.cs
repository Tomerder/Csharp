using System;

namespace MarshalingDifferences
{
    /// <summary>
    /// This class is used to retrieve information about objects that belong
    /// to a different application domain.  It contains a RectangleMBV and a
    /// RectangleMBR instance and allows for outputting their information, thus
    /// making the difference between MBV and MBR behavior evident.
    /// </summary>
    class DomainRepresentative : MarshalByRefObject
    {
        private RectangleMBV _rectMBV = new RectangleMBV(15, 15);
        private RectangleMBR _rectMBR = new RectangleMBR(15, 15);

        public RectangleMBV GetRectangleMBV()
        {
            return _rectMBV;
        }

        public void PrintRectangleMBV()
        {
            Console.WriteLine("Remote instance: " + _rectMBV);
        }

        public RectangleMBR GetRectangleMBR()
        {
            return _rectMBR;
        }

        public void PrintRectangleMBR()
        {
            Console.WriteLine("Remote instance: " + _rectMBR);
        }
    }
}
