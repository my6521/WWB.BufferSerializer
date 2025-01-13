using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class BooleanConverter : Converter<bool>
    {
        public override bool Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var val = byteBlock.ReadByte();

            return val == 1;
        }

        public override void Write(bool value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byte val = (byte)(value ? 0x01 : 0x00);
            byteBlock.WriteByte(val);
        }
    }
}