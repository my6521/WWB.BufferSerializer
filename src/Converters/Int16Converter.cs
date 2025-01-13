using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class Int16Converter : Converter<short>
    {
        public override short Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadInt16();
        }

        public override void Write(short value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteInt16(value);
        }
    }
}