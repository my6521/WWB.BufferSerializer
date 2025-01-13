﻿using WWB.BufferSeralizer.Data;

namespace WWB.BufferSeralizer.Converters
{
    public abstract class Converter<T>
    {
        private static readonly Converter<T> _converter = ConverterCache.GetConverter<T>();

        public static Converter<T> Default
        {
            get { return _converter; }
        }

        public static Converter<T> GetConverter(Type type)
        {
            if (type == null) return _converter;

            return ConverterCache.GetConverter<T>(type);
        }

        public abstract void Write(T value, ByteBlock byteBlock, int size);

        public abstract T Read(ByteBlock byteBlock, int size);
    }
}