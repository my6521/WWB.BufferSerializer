using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class Int16Converter : Converter<short>
    {
        public override short Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadInt16();
        }

        public override void Write(short value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteInt16(value);
        }
    }
}