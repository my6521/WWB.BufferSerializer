using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class DateTimeConverter : Converter<DateTime>
    {
        public override DateTime Read(ByteBlock byteBlock, int size)
        {
            var timeStamp = byteBlock.ReadUInt32();
            return Helpers.UnixTimeToDateTime(timeStamp);
        }

        public override void Write(DateTime value, ByteBlock byteBlock, int size)
        {
            var val = Helpers.DateTimeToUnixTime(value);
            byteBlock.WriteUInt32(val);
        }
    }
}