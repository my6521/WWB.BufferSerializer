using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class DoubleConverter : Converter<double>
    {
        public override double Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadDouble();
        }

        public override void Write(double value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteDouble(value);
        }
    }
}