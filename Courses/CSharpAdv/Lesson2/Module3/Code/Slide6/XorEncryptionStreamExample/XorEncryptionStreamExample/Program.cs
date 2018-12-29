using System;
using System.IO;
using System.Text;

namespace XorEncryptionStreamExample
{
    class Program
    {
        /// <summary>
        /// Demonstrates the use of the XorEncryptionStream decorator (see above)
        /// for encrypting and decrypting data written to a file stream.
        /// Note that a StreamWriter/StreamReader pair would also work with a
        /// XorEncryptionStream as the underlying stream, enabling very powerful
        /// scenarios for combining stream decorators with other stream functionality.
        /// </summary>
        static void Main(string[] args)
        {
            byte[] buf = Encoding.ASCII.GetBytes("Hello World" + Environment.NewLine);

            FileStream data = new FileStream("encrypted.dat", FileMode.Create);
            XorEncryptionStream encryptor = new XorEncryptionStream(data, 37);
            encryptor.Write(buf, 0, buf.Length);
            encryptor.Close();

            data = new FileStream("encrypted.dat", FileMode.Open);
            XorEncryptionStream decryptor = new XorEncryptionStream(data, 37);
            Console.WriteLine("Bytes read: " + decryptor.Read(buf, 0, buf.Length));
            decryptor.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buf));
        }
    }
}
