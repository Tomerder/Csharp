using System;
using System.IO;
using System.Linq;

namespace CompositeOutputStreamExample
{
    /// <summary>
    /// A stream that automatically dispatches output requests (writes) to multiple
    /// other streams, enabling a composite-like abstraction of a stream.
    /// Each of the operations in this class override an abstract method or property
    /// of the Stream class and implement it with regard to multiple output destinations.
    /// Read-related operations and the Seek operation are not supported.
    /// </summary>
    public class CompositeOutputStream : Stream
    {
        private readonly Stream[] _streams;

        public CompositeOutputStream(params Stream[] streams)
        {
            _streams = streams;
        }

        public override void Close()
        {
            Array.ForEach(_streams, s => s.Close());
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            Array.ForEach(_streams, s => s.Flush());
        }

        public override long Length
        {
            get { return (from s in _streams select s.Length).Max(); }
        }

        public override long Position
        {
            get
            {
                return _streams.First().Position;
            }
            set
            {
                Array.ForEach(_streams, s => s.Position = value);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            Array.ForEach(_streams, s => s.SetLength(value));
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Array.ForEach(_streams, s => s.Write(buffer, offset, count));
        }
    }
}
