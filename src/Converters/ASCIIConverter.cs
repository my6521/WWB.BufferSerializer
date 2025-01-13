using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class ASCIIConverter : Converter<string>
    {
        public override string Read(ByteBlock byteBlock, int size)
        {
            var val = byteBlock.ReadBytes(size);
            return Helpers.ToASCIIString(val);
        }

        public override void Write(string value, ByteBlock byteBlock, int size)
        {
            var values = Helpers.GetASCIIBytes(value);
            byteBlock.WriteBytes(values);
        }
    }
}