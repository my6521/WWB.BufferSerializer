using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class DecimalConverter : Converter<decimal>
    {
        public override decimal Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadDecimal();
        }

        public override void Write(decimal value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteDecimal(value);
        }
    }
}