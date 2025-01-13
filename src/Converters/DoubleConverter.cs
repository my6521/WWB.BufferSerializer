﻿using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public class DoubleConverter : Converter<double>
    {
        public override double Read(ByteBlock byteBlock, int size)
        {
            return byteBlock.ReadDouble();
        }

        public override void Write(double value, ByteBlock byteBlock, int size)
        {
            byteBlock.WriteDouble(value);
        }
    }
}