using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class BcdDateTimeConverter : Converter<DateTime>
    {
        public override DateTime Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var val = byteBlock.ReadBytes(size);
            return BcdStringToDateTime(Helpers.ToHexString(val));
        }

        public override void Write(DateTime value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var hex = value.ToString("yyyyMMddHHmmss");
            var bytes = Helpers.HexToBytes(hex);
            byteBlock.WriteBytes(bytes);
        }

        private DateTime BcdStringToDateTime(string bcdString)
        {
            if (bcdString.Length < 14)
                throw new ArgumentException("BCD时间字符串必须是14位长度", nameof(bcdString));

            // 解析各时间部分（注意BCD编码中每两位对应一个十进制数）
            int year = int.Parse(bcdString.Substring(0, 4));     // 年：2015
            int month = int.Parse(bcdString.Substring(4, 2));    // 月：07
            int day = int.Parse(bcdString.Substring(6, 2));      // 日：22
            int hour = int.Parse(bcdString.Substring(8, 2));     // 时：13
            int minute = int.Parse(bcdString.Substring(10, 2));  // 分：16
            int second = int.Parse(bcdString.Substring(12, 2));  // 秒：15
            if (year == 0)
                return DateTime.MinValue;
            // 创建并返回DateTime对象
            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}