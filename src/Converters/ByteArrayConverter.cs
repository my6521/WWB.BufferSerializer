using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class ByteArrayConverter : Converter<byte[]>
    {
        public override byte[] Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadBytes(size);
        }

        public override void Write(byte[] value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteBytes(value);
        }
    }
}