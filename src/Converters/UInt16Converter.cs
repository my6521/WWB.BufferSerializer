﻿using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Converters
{
    public class UInt16Converter : Converter<ushort>
    {
        public override ushort Read(ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            return byteBlock.ReadUInt16();
        }

        public override void Write(ushort value, ByteBlock byteBlock, int size, int lengthPlaceSize)
        {
            byteBlock.WriteUInt16(value);
        }
    }
}