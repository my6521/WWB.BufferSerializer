using System.Reflection;
using WWB.BufferSerializer.Attributes;
using WWB.BufferSerializer.Relection;

namespace WWB.BufferSerializer.Internal
{
    public class FastProperty
    {
        public PropertyInfo PropertyInfo { get; private set; }
        public Type Type { get; private set; }
        public FastTypeCode TypeCode { get; private set; }
        public int Size { get; private set; }
        public int LengthPlaceSize { get; private set; }
        public MethodInfo AddMethod { get; private set; }
        public Type ArgType { get; private set; }
        public FastTypeCode ArgTypeCode { get; private set; }
        public int ArgSize { get; private set; }
        public Type TypeHandler { get; private set; }

        public bool HasLengthPlace => Size == 0;

        public FastProperty(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            Type = propertyInfo.PropertyType;
            TypeCode = GetTypeCode(propertyInfo.PropertyType);

            if (TypeCode == FastTypeCode.List)
            {
                ArgType = propertyInfo.PropertyType.GetGenericArguments()[0];
                ArgTypeCode = GetTypeCode(ArgType);
                AddMethod = propertyInfo.PropertyType.GetMethod("Add");
            }

            var attrs = propertyInfo.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (attr is FastPropertyAttribute FastPropertyAttribute)
                {
                    Size = FastPropertyAttribute.Size;
                    LengthPlaceSize = FastPropertyAttribute.LengthPlaceSize;
                    ArgSize = FastPropertyAttribute.ArgSize;
                    TypeHandler = FastPropertyAttribute.TypeHandler;
                }
            }
        }

        public void SetValue(ref object instance, object obj)
        {
            if (obj != null)
            {
                PropertyInfo.SetValue(instance, obj);
            }
        }

        public object GetValue(object instance)
        {
            return PropertyInfo.GetValue(instance);
        }

        private FastTypeCode GetTypeCode(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case System.TypeCode.Boolean:
                    return FastTypeCode.Boolean;

                case System.TypeCode.Char:
                    return FastTypeCode.Char;

                case System.TypeCode.SByte:
                    return FastTypeCode.SByte;

                case System.TypeCode.Byte:
                    return FastTypeCode.Byte;

                case System.TypeCode.Int16:
                    return FastTypeCode.Int16;

                case System.TypeCode.Int32:
                    return FastTypeCode.Int32;

                case System.TypeCode.Int64:
                    return FastTypeCode.Int64;

                case System.TypeCode.UInt16:
                    return FastTypeCode.UInt16;

                case System.TypeCode.UInt32:
                    return FastTypeCode.UInt32;

                case System.TypeCode.UInt64:
                    return FastTypeCode.UInt64;

                case System.TypeCode.Double:
                    return FastTypeCode.Double;

                case System.TypeCode.Single:
                    return FastTypeCode.Float;

                case System.TypeCode.Decimal:
                    return FastTypeCode.Decimal;

                case System.TypeCode.DateTime:
                    return FastTypeCode.DateTime;

                case System.TypeCode.String:
                    return FastTypeCode.String;

                default:
                    if (type == typeof(byte[]))
                    {
                        return FastTypeCode.ByteArray;
                    }

                    if (type.IsClass)
                    {
                        if (type.IsList())
                        {
                            return FastTypeCode.List;
                        }
                        else
                        {
                            return FastTypeCode.Class;
                        }
                    }

                    return FastTypeCode.Empty;
            }
        }
    }
}