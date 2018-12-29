using System.IO;

namespace BinaryExample
{
    class Program
    {
        /// <summary>
        /// Demonstrates how a matrix of 10x10 double-precision numbers
        /// can be serialized to a binary file using the BinaryWriter class
        /// and then deserialized from the file using the BinaryReader class.
        /// Make note of the fact the matrix dimensions are serialized so that
        /// later on the matrix can be recreated.
        /// </summary>
        static void Main(string[] args)
        {
            double[,] matrix = new double[10, 10];
            for (int i = 0; i < 10; ++i)
                for (int j = 0; j < 10; ++j)
                    matrix[i, j] = i * j;

            BinaryWriter writer = new BinaryWriter(new FileStream("matrix.dat", FileMode.Create));
            writer.Write(matrix.GetUpperBound(0)); // 10
            writer.Write(matrix.GetUpperBound(1)); // 10
            for (int i = 0; i < 10; ++i)
                for (int j = 0; j < 10; ++j)
                    writer.Write(matrix[i, j]);
            writer.Close();

            BinaryReader reader = new BinaryReader(new FileStream("matrix.dat", FileMode.Open));
            int dim0 = reader.ReadInt32();
            int dim1 = reader.ReadInt32();
            double[,] newMatrix = new double[dim0, dim1];
            for (int i = 0; i < dim0; ++i)
                for (int j = 0; j < dim1; ++j)
                    newMatrix[i, j] = reader.ReadDouble();
            reader.Close();
        }
    }
}
