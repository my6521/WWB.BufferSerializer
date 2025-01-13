using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class FloatConverter : Converter<float>
    {
        public override float Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadFloat();
        }

        public override void Write(float value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteFloat(value);
        }
    }
}