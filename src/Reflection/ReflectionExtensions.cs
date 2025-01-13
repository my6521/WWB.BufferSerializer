using System.Collections;

namespace WWB.BufferSerializer.Relection
{
    public static class ReflectionExtensions
    {
        public static bool IsList(this Type type)
        {
            return typeof(IList).IsAssignableFrom(type);
        }
    }
}