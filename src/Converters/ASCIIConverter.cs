using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class ASCIIConverter : Converter<string>
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
            return Helpers.ToASCIIString(val);
        }

        public override void Write(string value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var values = Helpers.GetASCIIBytes(value);
            if (size == 0)
            {
                byteBlock.WriteInt32(value.Length, lengthPlaceSize);
            }
            byteBlock.WriteBytes(values);
        }
    }
}