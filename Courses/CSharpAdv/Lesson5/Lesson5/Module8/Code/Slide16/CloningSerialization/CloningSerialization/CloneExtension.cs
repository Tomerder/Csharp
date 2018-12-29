using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CloningSerialization
{
    /// <summary>
    /// This sample demonstrates how cloning can be performed using serialization.
    /// This class contains a generic extension method called Clone which serializes
    /// its target to a MemoryStream and deserializes it back.  If the type is serializable,
    /// this should result in deep cloning of the entire graph referenced by the
    /// instance provided.
    /// </summary>
    public static class CloneExtension
    {
        [ThreadStatic]
        private static BinaryFormatter _formatter;

        public static T Clone<T>(this T obj)
        {
            if (_formatter == null)
            {
                _formatter = new BinaryFormatter();
            }

            using (MemoryStream stream = new MemoryStream())
            {
                _formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (T)_formatter.Deserialize(stream);
            }
        }
    }
}
