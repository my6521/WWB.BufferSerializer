using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class DecimalConverter : Converter<decimal>
    {
        public override decimal Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadDecimal();
        }

        public override void Write(decimal value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteDecimal(value);
        }
    }
}