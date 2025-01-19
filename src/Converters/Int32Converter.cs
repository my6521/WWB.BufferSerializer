using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class Int32Converter : Converter<int>
    {
        private int maxSize = 4;

        public override int Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size > 0 && size < maxSize)
            {
                return byteBlock.ReadInt32(size);
            }

            return byteBlock.ReadInt32();
        }

        public override void Write(int value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            if (size > 0 && size < maxSize)
            {
                byteBlock.WriteInt32(value, size);
                return;
            }

            byteBlock.WriteInt32(value);
        }
    }
}