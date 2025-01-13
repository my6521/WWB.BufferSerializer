using System.Collections.Concurrent;

namespace WWB.BufferSeralizer.Converters
{
    public static class ConverterCache
    {
        private static readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();

        static ConverterCache()
        {
            _cache.TryAdd(typeof(bool), new BooleanConverter());
            _cache.TryAdd(typeof(byte[]), new ByteArrayConverter());
            _cache.TryAdd(typeof(byte), new ByteConverter());
            _cache.TryAdd(typeof(DateTime), new DateTimeConverter());
            _cache.TryAdd(typeof(decimal), new DecimalConverter());
            _cache.TryAdd(typeof(double), new DoubleConverter());
            _cache.TryAdd(typeof(float), new FloatConverter());
            _cache.TryAdd(typeof(short), new Int16Converter());
            _cache.TryAdd(typeof(int), new Int32Converter());
            _cache.TryAdd(typeof(long), new Int64Converter());
            _cache.TryAdd(typeof(ushort), new UInt16Converter());
            _cache.TryAdd(typeof(uint), new UInt32Converter());
            _cache.TryAdd(typeof(ulong), new UInt64Converter());
            _cache.TryAdd(typeof(string), new StringConverter());
        }

        public static Converter<T> GetConverter<T>()
        {
            return GetConverter<T>(typeof(T));
        }

        public static Converter<T> GetConverter<T>(Type type)
        {
            if (_cache.TryGetValue(type, out var converter))
            {
                return (Converter<T>)converter;
            }

            var ret = (Converter<T>)Activator.CreateInstance(type);
            _cache.TryAdd(type, ret);

            return ret;
        }
    }
}