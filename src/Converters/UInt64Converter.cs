using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class UInt64Converter : Converter<ulong>
    {
        public override ulong Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadUInt64();
        }

        public override void Write(ulong value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteUInt64(value);
        }
    }
}