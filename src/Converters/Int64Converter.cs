using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class Int64Converter : Converter<long>
    {
        private int maxSize = 8;

        public override long Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size > 0 && size < maxSize)
            {
                return byteBlock.ReadInt64(size);
            }
            return byteBlock.ReadInt64();
        }

        public override void Write(long value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size > 0 && size < maxSize)
            {
                byteBlock.WriteInt64(value, size);
                return;
            }
            byteBlock.WriteInt64(value);
        }
    }
}