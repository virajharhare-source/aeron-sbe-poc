using Adaptive.Agrona;
using Adaptive.Agrona.Concurrent;

namespace AeronSBE.Messages
{
    public class MessageHeaderDecoder
    {
        private IDirectBuffer _buffer;
        private int _offset;

        public MessageHeaderDecoder Wrap(IDirectBuffer buffer, int offset)
        {
            _buffer = buffer;
            _offset = offset;
            return this;
        }

        public short BlockLength => _buffer.GetShort(_offset, ByteOrder.LittleEndian);
        public short TemplateId => _buffer.GetShort(_offset + 2, ByteOrder.LittleEndian);
        public short SchemaId => _buffer.GetShort(_offset + 4, ByteOrder.LittleEndian);
        public short Version => _buffer.GetShort(_offset + 6, ByteOrder.LittleEndian);
    }
}
