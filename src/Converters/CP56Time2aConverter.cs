using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class CP56Time2aConverter : Converter<DateTime>
    {
        public override DateTime Read(ByteBlock byteBlock, int size)
        {
            var val = byteBlock.ReadBytes(7);
            return Helpers.CP56Time2aToDateTime(val, 0, 7);
        }

        public override void Write(DateTime value, ByteBlock byteBlock, int size)
        {
            var dateTime = Helpers.DateTimeToCP56Time2aHexString(value);
            var bytes = Helpers.HexToBytes(dateTime);
            byteBlock.WriteBytes(bytes);
        }
    }
}