using Adaptive.Agrona;
using Adaptive.Agrona.Concurrent;

namespace AeronSBE.Messages
{
    public class MessageHeaderEncoder
    {
        public const int ENCODED_LENGTH = 8;

        private UnsafeBuffer _buffer;
        private int _offset;

        public MessageHeaderEncoder Wrap(UnsafeBuffer buffer, int offset)
        {
            _buffer = buffer;
            _offset = offset;
            return this;
        }

        public MessageHeaderEncoder BlockLength(short value)
        {
            _buffer.PutShort(_offset, value, ByteOrder.LittleEndian);
            return this;
        }

        public MessageHeaderEncoder TemplateId(short value)
        {
            _buffer.PutShort(_offset + 2, value, ByteOrder.LittleEndian);
            return this;
        }

        public MessageHeaderEncoder SchemaId(short value)
        {
            _buffer.PutShort(_offset + 4, value, ByteOrder.LittleEndian);
            return this;
        }

        public MessageHeaderEncoder Version(short value)
        {
            _buffer.PutShort(_offset + 6, value, ByteOrder.LittleEndian);
            return this;
        }
    }
}
