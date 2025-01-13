using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class ByteConverter : Converter<byte>
    {
        public override byte Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadByte();
        }

        public override void Write(byte value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteByte(value);
        }
    }
}