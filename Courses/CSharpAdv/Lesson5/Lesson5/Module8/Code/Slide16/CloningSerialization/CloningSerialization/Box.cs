using System;

namespace CloningSerialization
{
    /// <summary>
    /// A sample type which implements ICloneable by using the extension
    /// method implemented above.
    /// </summary>
    [Serializable]
    class Box : ICloneable
    {
        public string Name { get; set; }
        public float Volume { get; set; }

        public object Clone()
        {
            return this.Clone<Box>();
        }

        public override string ToString()
        {
            return Name + " " + Volume;
        }
    }
}
