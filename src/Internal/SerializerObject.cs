using System.Reflection;
using WWB.BufferSeralizer.Attributes;
using WWB.BufferSeralizer.Internal;

namespace WWB.BufferSeralizer.Relection
{
    public class SerializerObject
    {
        public Type Type { get; private set; }
        public List<FastProperty> Properties { get; private set; } = new List<FastProperty>();

        public SerializerObject(Type type)
        {
            Type = type;
            var properties = GetProperties(type);
            foreach (var property in properties)
            {
                var entityProperty = new FastProperty(property);
                Properties.Add(entityProperty);
            }
        }

        private static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default)
                          .Where(p =>
                          {
                              if (p.IsDefined(typeof(FastPropertyAttribute), true))
                              {
                                  return true;
                              }

                              return false;
                          }).ToArray();
        }
    }
}