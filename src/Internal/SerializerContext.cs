using System.Collections.Concurrent;

namespace WWB.BufferSerializer.Relection
{
    internal static class SerializerContext
    {
        private static readonly ConcurrentDictionary<Type, SerializerObject> _instanceCache = new ConcurrentDictionary<Type, SerializerObject>();

        public static object GetNewInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static SerializerObject GetSerializeObject(Type type)
        {
            if (_instanceCache.TryGetValue(type, out SerializerObject instanceObject))
            {
                return instanceObject;
            }
            instanceObject = new SerializerObject(type);
            _instanceCache.TryAdd(type, instanceObject);

            return instanceObject;
        }
    }
}