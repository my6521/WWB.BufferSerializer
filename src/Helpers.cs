using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WWB.BufferSeralizer
{
    public static class Helpers
    {
        /// <summary>
        /// 日期转换为UnixTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static uint DateTimeToUnixTime(DateTime date)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timeSpan = date.ToUniversalTime().Subtract(start);

            return (uint)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// UnixTime转换为日期转换
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToDateTime(uint unixTime)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dateTime = startTime.AddSeconds(unixTime);
            return dateTime.ToLocalTime();
        }

        /// <summary>
        /// CP56Time2a格式转日期格式
        /// </summary>
        /// <returns></returns>
        public static DateTime CP56Time2aToDateTime(byte[] buff, int start, int len)
        {
            var data = new byte[len];
            Buffer.BlockCopy(buff, start, data, 0, data.Length);
            var timeTxt = ToDateString(data);
            if (DateTime.TryParse(timeTxt, out var date))
            {
                return date;
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 日期格式转换为CP56Time2a
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToCP56Time2aHexString(DateTime date)
        {
            var str = date.ToString("yy-MM-dd HH:mm:ss");

            var time = str.Split(' ');
            var ymd = time[0].Split('-');
            var hms = time[1].Split(':');
            var t1 = Convert.ToString(int.Parse(hms[2]) * 1000, 16).PadLeft(4, '0');
            var t11 = t1.Substring(0, 2);
            var t12 = t1.Substring(2, 2);
            var t2 = Convert.ToString(int.Parse(hms[1]), 16).PadLeft(2, '0');
            var t3 = Convert.ToString(int.Parse(hms[0]), 16).PadLeft(2, '0');
            var t4 = Convert.ToString(int.Parse(ymd[2]), 16).PadLeft(2, '0');
            var t5 = Convert.ToString(int.Parse(ymd[1]), 16).PadLeft(2, '0');
            var t6 = Convert.ToString(int.Parse(ymd[0]), 16).PadLeft(2, '0');
            string hex = (t12 + t11 + t2 + t3 + t4 + t5 + t6).ToUpper();

            return hex;
        }

        private static string ToDateString(byte[] bytes)
        {
            var milliseconds1 = bytes[0];
            var milliseconds2 = bytes[1];
            var milliseconds = milliseconds2 * 256 + milliseconds1;
            // 位于 0011 1111
            var minutes = bytes[2] & 0x3F;
            // 位于 0001 1111
            var hours = bytes[3] & 0x1F;
            // 位于 0001 1111
            var days = bytes[4] & 0x1F;
            // 位于 0000 1111
            var months = bytes[5] & 0x0F;
            // 位于 0111 1111
            var years = bytes[6] & 0x7F;

            var y = years.ToString().PadLeft(2, '0');
            var m = months.ToString().PadLeft(2, '0');
            var d = days.ToString().PadLeft(2, '0');
            var h = hours.ToString().PadLeft(2, '0');
            var min = minutes.ToString().PadLeft(2, '0');
            var s = (milliseconds / 1000).ToString().PadLeft(2, '0');

            return $"20{y}-{m}-{d} {h}:{min}:{s}";
        }

        /// <summary>
        /// 字节转ASCII
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static string ToASCIIString(byte[] buff)
        {
            return ToASCIIString(buff, 0, buff.Length);
        }

        /// <summary>
        /// 字节转ASCII
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ToASCIIString(byte[] buff, int start, int len)
        {
            return Encoding.ASCII.GetString(buff, start, len).Replace('\0', '0');
        }

        /// <summary>
        /// ASCII转字节
        /// </summary>
        /// <param name="ascii"></param>
        /// <returns></returns>
        public static byte[] GetASCIIBytes(string ascii)
        {
            return Encoding.ASCII.GetBytes(ascii);
        }

        /// <summary>
        /// Hex字符串转字节
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string hex)
        {
            if (!IsHexString(hex))
            {
                throw new Exception("不是HEX字符串");
            }
            var strArray = Regex.Split(hex, "(?<=\\G.{2})(?!$)");
            var array = new byte[strArray.Length];
            for (var i = 0; i <= strArray.Length - 1; ++i)
                array[i] = byte.Parse(strArray[i], NumberStyles.HexNumber);

            return array;
        }

        public static bool IsHexString(string input)
        {
            // 使用正则表达式来判断是否为十六进制字符串
            string pattern = "^[0-9A-Fa-f]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}