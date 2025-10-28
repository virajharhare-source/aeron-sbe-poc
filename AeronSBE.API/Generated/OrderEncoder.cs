using Adaptive.Agrona;
using Adaptive.Agrona.Concurrent;

namespace AeronSBE.Messages
{
    public class OrderEncoder
    {
        public const int BLOCK_LENGTH = 20;
        public const short TEMPLATE_ID = 1;

        private UnsafeBuffer _buffer;
        private int _offset;

        public OrderEncoder Wrap(UnsafeBuffer buffer, int offset)
        {
            _buffer = buffer;
            _offset = offset;
            return this;
        }

        public OrderEncoder OrderId(long value)
        {
            _buffer.PutLong(_offset, value, ByteOrder.LittleEndian);
            return this;
        }

        public OrderEncoder Price(double value)
        {
            _buffer.PutDouble(_offset + 8, value);  // <--- no ByteOrder argument
            return this;
        }

        public OrderEncoder Quantity(int value)
        {
            _buffer.PutInt(_offset + 16, value, ByteOrder.LittleEndian);
            return this;
        }
    }
}
