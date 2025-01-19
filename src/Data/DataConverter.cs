using System.Buffers.Binary;

namespace WWB.BufferSerializer.Data
{
    public enum EndianType
    {
        Little,
        Big,
    }

    public abstract class DataConverter
    {
        private static readonly DataConverter SwapConv = new SwapConverter();
        private static readonly DataConverter CopyConv = new CopyConverter();

        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        public abstract double ToDouble(ReadOnlySpan<byte> data, int index);

        public abstract float ToFloat(ReadOnlySpan<byte> data, int index);

        public abstract decimal ToDecimal(ReadOnlySpan<byte> data, int index);

        public abstract short ToInt16(ReadOnlySpan<byte> data, int index);

        public abstract int ToInt32(ReadOnlySpan<byte> data, int index);

        public abstract long ToInt64(ReadOnlySpan<byte> data, int index);

        public abstract ushort ToUInt16(ReadOnlySpan<byte> data, int index);

        public abstract uint ToUInt32(ReadOnlySpan<byte> data, int index);

        public abstract ulong ToUInt64(ReadOnlySpan<byte> data, int index);

        public abstract int ToInt32(ReadOnlySpan<byte> data, int index, int len);

        public abstract long ToInt64(ReadOnlySpan<byte> data, int index, int len);

        public byte[] GetBytes(decimal value)
        {
            byte[] ret = new byte[16];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(double value)
        {
            byte[] ret = new byte[8];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(float value)
        {
            byte[] ret = new byte[4];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(short value)
        {
            byte[] ret = new byte[2];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(int value)
        {
            byte[] ret = new byte[4];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(int value, int len)
        {
            byte[] ret = new byte[len];
            PutBytes(ret, 0, value, len);
            return ret;
        }

        public byte[] GetBytes(long value)
        {
            byte[] ret = new byte[8];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(long value, int len)
        {
            byte[] ret = new byte[len];
            PutBytes(ret, 0, value, len);
            return ret;
        }

        public byte[] GetBytes(ushort value)
        {
            byte[] ret = new byte[2];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(uint value)
        {
            byte[] ret = new byte[4];
            PutBytes(ret, 0, value);
            return ret;
        }

        public byte[] GetBytes(ulong value)
        {
            byte[] ret = new byte[8];
            PutBytes(ret, 0, value);
            return ret;
        }

        public abstract void PutBytes(Span<byte> dest, int destIdx, double value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, float value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, decimal value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, short value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, int value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, long value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, ushort value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, uint value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, ulong value);

        public abstract void PutBytes(Span<byte> dest, int destIdx, int value, int len);

        public abstract void PutBytes(Span<byte> dest, int destIdx, long value, int len);

        public static DataConverter LittleEndian
        {
            get
            {
                return BitConverter.IsLittleEndian ? CopyConv : SwapConv;
            }
        }

        public static DataConverter BigEndian
        {
            get
            {
                return BitConverter.IsLittleEndian ? SwapConv : CopyConv;
            }
        }

        public static DataConverter Native
        {
            get
            {
                return CopyConv;
            }
        }

        public static DataConverter GetDataConverter(EndianType endianType)
        {
            switch (endianType)
            {
                case EndianType.Little:
                    return LittleEndian;

                case EndianType.Big:
                    return BigEndian;

                default:
                    return default;
            }
        }

        internal void Check(Span<byte> dest, int destIdx, int size)
        {
            if (dest == null)
                throw new ArgumentNullException("dest");
            if (destIdx < 0 || destIdx > dest.Length - size)
                throw new ArgumentException("destIdx");
        }

        private class CopyConverter : DataConverter
        {
            public override short ToInt16(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 2)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadInt16LittleEndian(data.Slice(index, 2));
            }

            public override int ToInt32(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadInt32LittleEndian(data.Slice(index, 4));
            }

            public override long ToInt64(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadInt64LittleEndian(data.Slice(index, 8));
            }

            public override ushort ToUInt16(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 2)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(index, 2));
            }

            public override uint ToUInt32(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(index, 4));
            }

            public override ulong ToUInt64(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(index, 8));
            }

            public override float ToFloat(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadSingleLittleEndian(data.Slice(index, 4));
            }

            public override double ToDouble(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadDoubleLittleEndian(data.Slice(index, 8));
            }

            public override decimal ToDecimal(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 16)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                int[] bits = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    bits[i] = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(i * 4 + index, 4));
                }

                return new decimal(bits);
            }

            public override int ToInt32(ReadOnlySpan<byte> data, int index, int len)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < len)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                var res = 0;
                const int bit = 8;
                for (var i = 0; i < len; i++)
                {
                    var offset = i * bit;
                    res |= (data[index + i] & byte.MaxValue) << offset;
                }
                return res;
            }

            public override long ToInt64(ReadOnlySpan<byte> data, int index, int len)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < len)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                long res = 0;
                const int bit = 8;
                for (var i = 0; i < len; i++)
                {
                    var offset = i * bit;
                    res |= (data[index + i] & byte.MaxValue) << offset;
                }
                return res;
            }

            public override void PutBytes(Span<byte> dest, int destIdx, double value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteDoubleLittleEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, float value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteSingleLittleEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, decimal value)
            {
                Check(dest, destIdx, 16);
                int[] bits = decimal.GetBits(value);

                for (int i = 0; i < 4; i++)
                {
                    BinaryPrimitives.WriteInt32LittleEndian(dest.Slice(i * 4 + destIdx, 4), bits[i]);
                }
            }

            public override void PutBytes(Span<byte> dest, int destIdx, short value)
            {
                Check(dest, destIdx, 2);
                BinaryPrimitives.WriteInt16LittleEndian(dest.Slice(destIdx, 2), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, int value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteInt32LittleEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, long value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteInt64LittleEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, ushort value)
            {
                Check(dest, destIdx, 2);
                BinaryPrimitives.WriteUInt16LittleEndian(dest.Slice(destIdx, 2), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, uint value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteUInt32LittleEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, ulong value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteUInt64LittleEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, int value, int len)
            {
                Check(dest, destIdx, len);
                for (var i = 0; i < len; ++i)
                {
                    dest[destIdx + i] = (byte)(value >> i * 8 & byte.MaxValue);
                }
            }

            public override void PutBytes(Span<byte> dest, int destIdx, long value, int len)
            {
                Check(dest, destIdx, len);
                for (var i = 0; i < len; ++i)
                {
                    dest[destIdx + i] = (byte)(value >> i * 8 & byte.MaxValue);
                }
            }
        }

        private class SwapConverter : DataConverter
        {
            public override short ToInt16(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 2)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadInt16BigEndian(data.Slice(index, 2));
            }

            public override int ToInt32(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadInt32BigEndian(data.Slice(index, 4));
            }

            public override long ToInt64(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");
                return BinaryPrimitives.ReadInt64BigEndian(data.Slice(index, 8));
            }

            public override ushort ToUInt16(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 2)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt16BigEndian(data.Slice(index, 2));
            }

            public override uint ToUInt32(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt32BigEndian(data.Slice(index, 4));
            }

            public override ulong ToUInt64(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadUInt64BigEndian(data.Slice(index, 8));
            }

            public override float ToFloat(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 4)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadSingleBigEndian(data.Slice(index, 4));
            }

            public override double ToDouble(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 8)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                return BinaryPrimitives.ReadDoubleBigEndian(data.Slice(index, 8));
            }

            public override decimal ToDecimal(ReadOnlySpan<byte> data, int index)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < 16)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");
                int[] bits = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    bits[i] = BinaryPrimitives.ReadInt32BigEndian(data.Slice(i * 4 + index, 4));
                }

                return new decimal(bits);
            }

            public override int ToInt32(ReadOnlySpan<byte> data, int index, int len)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < len)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                var res = 0;
                const int bit = 8;
                for (var i = 0; i < len; i++)
                {
                    var offset = (len - i - 1) * bit;
                    res |= (data[index + i] & byte.MaxValue) << offset;
                }
                return res;
            }

            public override long ToInt64(ReadOnlySpan<byte> data, int index, int len)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (data.Length - index < len)
                    throw new ArgumentException("index");
                if (index < 0)
                    throw new ArgumentException("index");

                long res = 0;
                const int bit = 8;
                for (var i = 0; i < len; i++)
                {
                    var offset = (len - i - 1) * bit;
                    res |= (data[index + i] & byte.MaxValue) << offset;
                }
                return res;
            }

            public override void PutBytes(Span<byte> dest, int destIdx, double value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteDoubleBigEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, float value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteSingleBigEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, decimal value)
            {
                Check(dest, destIdx, 16);
                int[] bits = decimal.GetBits(value);

                for (int i = 0; i < 4; i++)
                {
                    BinaryPrimitives.WriteInt32BigEndian(dest.Slice(i * 4 + destIdx, 4), bits[i]);
                }
            }

            public override void PutBytes(Span<byte> dest, int destIdx, short value)
            {
                Check(dest, destIdx, 2);
                BinaryPrimitives.WriteInt16BigEndian(dest.Slice(destIdx, 2), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, int value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteInt32BigEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, long value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteInt64BigEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, ushort value)
            {
                Check(dest, destIdx, 2);
                BinaryPrimitives.WriteUInt16BigEndian(dest.Slice(destIdx, 2), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, uint value)
            {
                Check(dest, destIdx, 4);
                BinaryPrimitives.WriteUInt32BigEndian(dest.Slice(destIdx, 4), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, ulong value)
            {
                Check(dest, destIdx, 8);
                BinaryPrimitives.WriteUInt64BigEndian(dest.Slice(destIdx, 8), value);
            }

            public override void PutBytes(Span<byte> dest, int destIdx, int value, int len)
            {
                Check(dest, destIdx, len);
                int endIdx = destIdx + len - 1;
                for (var i = 0; i < len; ++i)
                {
                    dest[endIdx - i] = (byte)(value >> i * 8 & byte.MaxValue);
                }
            }

            public override void PutBytes(Span<byte> dest, int destIdx, long value, int len)
            {
                Check(dest, destIdx, len);
                int endIdx = destIdx + len - 1;
                for (var i = 0; i < len; ++i)
                {
                    dest[endIdx - i] = (byte)(value >> i * 8 & byte.MaxValue);
                }
            }
        }
    }
}