using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class DateTimeConverter : Converter<DateTime>
    {
        public override DateTime Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var timeStamp = byteBlock.ReadUInt32();
            return Helpers.UnixTimeToDateTime(timeStamp);
        }

        public override void Write(DateTime value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var val = Helpers.DateTimeToUnixTime(value);
            byteBlock.WriteUInt32(val);
        }
    }
}