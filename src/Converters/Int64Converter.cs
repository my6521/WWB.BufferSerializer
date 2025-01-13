using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class Int64Converter : Converter<long>
    {
        public override long Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadInt64();
        }

        public override void Write(long value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteInt64(value);
        }
    }
}