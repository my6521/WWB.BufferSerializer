using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class StringConverter : Converter<string>
    {
        public override string Read(ByteBlock byteBlock, int size)
        {
            var val = byteBlock.ReadBytes(size);
            return Helpers.ToHexString(val);
        }

        public override void Write(string value, ByteBlock byteBlock, int size)
        {
            var hexLen = size * 2;
            if (value.Length != hexLen)
            {
                throw new Exception($"hex字符串长度应为:{hexLen}");
            }
            var val = Helpers.HexToBytes(value);
            byteBlock.WriteBytes(val);
        }
    }
}