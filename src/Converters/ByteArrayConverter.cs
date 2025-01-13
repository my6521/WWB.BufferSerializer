using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class ByteArrayConverter : Converter<byte[]>
    {
        public override byte[] Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size == 0)
            {
                size = byteBlock.ReadInt32(lengthPlaceSize);
            }

            return byteBlock.ReadBytes(size);
        }

        public override void Write(byte[] value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size == 0)
            {
                byteBlock.WriteInt32(value.Length, lengthPlaceSize);
            }
            byteBlock.WriteBytes(value);
        }
    }
}