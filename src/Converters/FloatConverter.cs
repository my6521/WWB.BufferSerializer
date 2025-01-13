using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class FloatConverter : Converter<float>
    {
        public override float Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadFloat();
        }

        public override void Write(float value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteFloat(value);
        }
    }
}