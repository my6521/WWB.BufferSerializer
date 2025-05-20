using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class BcdTimeSpanConverter : Converter<TimeSpan>
    {
        public override TimeSpan Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var val = byteBlock.ReadBytes(2);
            return BcdStringToTimeSpan(Helpers.ToHexString(val));
        }

        public override void Write(TimeSpan value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            var hex = value.ToString("hh\\:mm");
            var bytes = Helpers.HexToBytes(hex);
            byteBlock.WriteBytes(bytes);
        }

        private TimeSpan BcdStringToTimeSpan(string bcdString)
        {
            if (bcdString.Length < 4)
                throw new ArgumentException("BCD时间字符串必须是4位长度", nameof(bcdString));

            // 解析各时间部分（注意BCD编码中每两位对应一个十进制数）
            int hour = int.Parse(bcdString.Substring(0, 2));     // 时：13
            int minute = int.Parse(bcdString.Substring(2, 2));  // 分：16
            int second = 0;

            // 创建并返回DateTime对象
            return new TimeSpan(hour, minute, second);
        }
    }
}