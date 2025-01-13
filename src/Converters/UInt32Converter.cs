using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class UInt32Converter : Converter<uint>
    {
        public override uint Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadUInt32();
        }

        public override void Write(uint value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteUInt32(value);
        }
    }
}