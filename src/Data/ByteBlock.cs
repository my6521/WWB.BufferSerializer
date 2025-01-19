namespace WWB.BufferSerializer.Data
{
    public class ByteBlock : IByteBlock
    {
        private byte[] _data;
        private int _length;
        private int _position = 0;
        private EndianType _endianType;

        public ByteBlock(byte[] bytes, EndianType endianType = EndianType.Little)
        {
            _data = bytes ?? throw new ArgumentNullException(nameof(bytes));
            _length = bytes.Length;
            _endianType = endianType;
        }

        public ByteBlock(int byteSize, EndianType endianType = EndianType.Little)
        {
            _data = new byte[byteSize];
            _endianType = endianType;
        }

        public int CanReadLength => this._length - this._position;
        public bool CanRead => this.CanReadLength > 0;
        public int Length => this._length;

        public int Position
        {
            get => this._position;
            set => this._position = value;
        }

        public byte this[int index]
        {
            get
            {
                return this._data[index];
            }
            set
            {
                this._data[index] = value;
            }
        }

        public void Seek(int position)
        {
            this._position = position;
        }

        public void SeekToEnd()
        {
            this._position = this._length;
        }

        public void SeekToStart()
        {
            this._position = 0;
        }

        public byte[] GetData()
        {
            return _data.AsSpan().Slice(0, this._length).ToArray();
        }

        public byte[] ReadBytes(int len)
        {
            if (this.CanReadLength < len)
            {
                throw new ArgumentException();
            }

            byte[] value = new byte[len];
            Buffer.BlockCopy(this._data, this._position, value, 0, len);
            this._position += len;
            return value;
        }

        public byte ReadByte()
        {
            var size = 1;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }

            var value = this._data[this._position];
            this._position += size;
            return value;
        }

        public short ReadInt16()
        {
            var size = 2;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToInt16(this._data, this._position);
            this._position += size;
            return value;
        }

        public ushort ReadUInt16()
        {
            var size = 2;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToUInt16(this._data, this._position);
            this._position += size;
            return value;
        }

        public int ReadInt32()
        {
            var size = 4;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToInt32(this._data, this._position);
            this._position += size;
            return value;
        }

        public int ReadInt32(int len)
        {
            var size = len;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToInt32(this._data, this._position, len);
            this._position += size;
            return value;
        }

        public uint ReadUInt32()
        {
            var size = 4;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToUInt32(this._data, this._position);
            this._position += size;
            return value;
        }

        public long ReadInt64()
        {
            var size = 8;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToInt64(this._data, this._position);
            this._position += size;
            return value;
        }

        public long ReadInt64(int len)
        {
            var size = len;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToInt64(this._data, this._position, len);
            this._position += size;
            return value;
        }

        public ulong ReadUInt64()
        {
            var size = 8;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToUInt64(this._data, this._position);
            this._position += size;
            return value;
        }

        public float ReadFloat()
        {
            int size = 4;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToFloat(this._data, this._position);
            this._position += size;
            return value;
        }

        public double ReadDouble()
        {
            int size = 8;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToDouble(this._data, this._position);
            this._position += size;
            return value;
        }

        public decimal ReadDecimal()
        {
            int size = 16;
            if (this.CanReadLength < size)
            {
                throw new ArgumentException();
            }
            var value = DataConverter.GetDataConverter(_endianType).ToDecimal(this._data, this._position);
            this._position += size;
            return value;
        }

        public void WriteBytes(byte[] value)
        {
            var size = value.Length;
            Buffer.BlockCopy(value, 0, this._data, this._position, size);
            this._position += size;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteByte(byte value)
        {
            this._data[this._position++] = value;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteInt16(short value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 2;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteUInt16(ushort value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 2;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteInt32(int value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 4;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteInt32(int value, int len)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value, len);
            this._position += len;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteUInt32(uint value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 4;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteInt64(long value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 8;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteInt64(long value, int len)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value, len);
            this._position += len;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteUInt64(ulong value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 8;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteFloat(float value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 4;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteDouble(double value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 8;
            this._length = this._position > this._length ? this._position : this._length;
        }

        public void WriteDecimal(decimal value)
        {
            DataConverter.GetDataConverter(_endianType).PutBytes(this._data, this._position, value);
            this._position += 16;
            this._length = this._position > this._length ? this._position : this._length;
        }
    }
}