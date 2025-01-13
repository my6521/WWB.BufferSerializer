using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class UInt64Converter : Converter<ulong>
    {
        public override ulong Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadUInt64();
        }

        public override void Write(ulong value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteUInt64(value);
        }
    }
}