using Adaptive.Agrona;
using Adaptive.Agrona.Concurrent;

namespace AeronSBE.Messages
{
    public class OrderDecoder
    {
        private IDirectBuffer _buffer;
        private int _offset;

        public OrderDecoder Wrap(IDirectBuffer buffer, int offset)
        {
            _buffer = buffer;
            _offset = offset;
            return this;
        }

        public long OrderId => _buffer.GetLong(_offset);
        public double Price => _buffer.GetDouble(_offset + 8);  // <--- no ByteOrder argument
        public int Quantity => _buffer.GetInt(_offset + 16);
    }
}
