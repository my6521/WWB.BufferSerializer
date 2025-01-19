namespace WWB.BufferSerializer.Data
{
    public interface IByteBlock
    {
        int Length { get; }
        int Position { get; set; }
        bool CanRead { get; }
        int CanReadLength { get; }

        byte this[int index] { get; set; }

        void Seek(int position);

        void SeekToEnd();

        void SeekToStart();

        byte[] GetData();

        byte[] ReadBytes(int len);

        byte ReadByte();

        short ReadInt16();

        ushort ReadUInt16();

        int ReadInt32();

        int ReadInt32(int len);

        uint ReadUInt32();

        long ReadInt64();

        long ReadInt64(int len);

        ulong ReadUInt64();

        float ReadFloat();

        double ReadDouble();

        decimal ReadDecimal();

        void WriteBytes(byte[] value);

        void WriteByte(byte value);

        void WriteInt16(short value);

        void WriteUInt16(ushort value);

        void WriteInt32(int value);

        void WriteInt32(int value, int len);

        void WriteUInt32(uint value);

        void WriteInt64(long value);

        void WriteInt64(long value, int len);

        void WriteUInt64(ulong value);

        void WriteFloat(float value);

        void WriteDouble(double value);

        void WriteDecimal(decimal value);
    }
}