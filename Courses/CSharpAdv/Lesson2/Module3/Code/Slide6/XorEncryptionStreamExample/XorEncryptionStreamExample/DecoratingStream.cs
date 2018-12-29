using System.IO;

namespace XorEncryptionStreamExample
{
    /// <summary>
    /// Serves as a base for all stream decorators.  Implements all
    /// abstract methods from the Stream class by delegating the work to
    /// the underlying stream (DecoratedStream) which is passed to the
    /// constructor.
    /// Derived classes are expected to override the relevant functionality
    /// while delegating the rest of the work to the decorated stream
    /// as in the base class.
    /// </summary>
    public abstract class DecoratingStream : Stream
    {
        private readonly Stream _stream;

        protected Stream DecoratedStream { get { return _stream; } }

        protected DecoratingStream(Stream stream)
        {
            _stream = stream;
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get
            {
                return _stream.Position;
            }
            set
            {
                _stream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            base.Close();
            _stream.Close();
        }
    }
}
