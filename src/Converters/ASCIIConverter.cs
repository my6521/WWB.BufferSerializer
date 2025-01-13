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
            if (size > 0)
            {
                var val = byteBlock.ReadBytes(size);
                return Helpers.ToASCIIString(val);
            }

            return null;
        }

        public override void Write(string value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size == 0)
            {
                byteBlock.WriteInt32(value.Length, lengthPlaceSize);
            }
            var values = Helpers.GetASCIIBytes(value);
            byteBlock.WriteBytes(values);
        }
    }
}