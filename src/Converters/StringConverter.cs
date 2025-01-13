using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class StringConverter : Converter<string>
    {
        public override string Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size == 0)
            {
                size = byteBlock.ReadInt32(lengthPlaceSize);
            }
            if (size == 0)
            {
                throw new ArgumentException("size需大于0");
            }
            var val = byteBlock.ReadBytes(size);
            return Helpers.ToHexString(val);
        }

        public override void Write(string value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var hexLen = size * 2;
            if (value.Length != hexLen)
            {
                throw new Exception($"hex字符串长度应为:{hexLen}");
            }
            if (size == 0)
            {
                byteBlock.WriteInt32(value.Length, lengthPlaceSize);
            }
            var val = Helpers.HexToBytes(value);
            byteBlock.WriteBytes(val);
        }
    }
}