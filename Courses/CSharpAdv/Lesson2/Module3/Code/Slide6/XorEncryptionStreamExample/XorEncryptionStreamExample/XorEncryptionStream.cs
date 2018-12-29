
using System.IO;
namespace XorEncryptionStreamExample
{
    /// <summary>
    /// Implements simple XOR-based encryption and decryption over data
    /// written to and read from an underlying stream.  Delegation of most
    /// work is performed by inheritance from the DecoratingStream class.
    /// Only the Read and Write operations are overriden, and they perform
    /// the decryption and encryption respectively using an "encryption key"
    /// passed to the stream's constructor.
    /// </summary>
    public class XorEncryptionStream : DecoratingStream
    {
        private readonly byte _key;

        public XorEncryptionStream(Stream stream, byte key)
            : base(stream)
        {
            _key = key;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = base.Read(buffer, offset, count);
            for (int i = offset; i < offset + read; ++i)
                buffer[i] ^= _key;
            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = offset; i < offset + count; ++i)
                buffer[i] ^= _key;
            base.Write(buffer, offset, count);
        }
    }
}
