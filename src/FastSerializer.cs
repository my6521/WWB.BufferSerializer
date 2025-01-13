using System.Collections;
using System.Reflection;
using WWB.BufferSeralizer.Attributes;
using WWB.BufferSeralizer.Converters;
using WWB.BufferSeralizer.Data;
using WWB.BufferSeralizer.Internal;
using WWB.BufferSeralizer.Relection;

namespace WWB.BufferSeralizer
{
    public class FastSerializer
    {
        #region Serialize

        public static byte[] SerializeObject<T>(T obj) where T : new()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var type = typeof(T);
            var attr = GetContractAttr(type);
            var serializerObject = SerializerContext.GetSerializeObject(type);
            var byteBlock = new ByteBlock(attr.Size, attr.EndianType);
            SerializeObject(obj, byteBlock, serializerObject);
            return byteBlock.GetData();
        }

        private static void SerializeObject(object obj, ByteBlock byteBlock, SerializerObject serializerObject)
        {
            foreach (var property in serializerObject.Properties)
            {
                var value = property.GetValue(obj);
                if (property.TypeCode == FastTypeCode.Class)
                {
                    var thisSerializerObject = SerializerContext.GetSerializeObject(property.Type);
                    SerializeObject(value, byteBlock, thisSerializerObject);
                }
                else if (property.TypeCode == FastTypeCode.List)
                {
                    SerializeList(value, byteBlock, property);
                }
                else
                {
                    Serialize(property.Type, value, byteBlock, property.Size, property.TypeHandler);
                }
            }
        }

        private static void SerializeList(object obj, ByteBlock byteBlock, FastProperty property)
        {
            var list = (IList)obj;
            if (property.HasLengthPlace)
            {
                byteBlock.WriteInt32(list.Count, property.LengthPlaceSize);
            }
            if (property.ArgTypeCode == FastTypeCode.Class)
            {
                var thisSerializerObject = SerializerContext.GetSerializeObject(property.ArgType);
                foreach (var item in list)
                {
                    SerializeObject(item, byteBlock, thisSerializerObject);
                }
            }
            else
            {
                foreach (var item in list)
                {
                    Serialize(property.ArgType, item, byteBlock, property.ArgSize, property.TypeHandler);
                }
            }
        }

        private static void Serialize(Type type, object obj, ByteBlock byteBlock, int size, Type handlerType)
        {
            switch (obj)
            {
                case bool value:
                    {
                        Converter<bool>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case byte value:
                    {
                        Converter<byte>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case short value:
                    {
                        Converter<short>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case ushort value:
                    {
                        Converter<ushort>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case int value:
                    {
                        Converter<int>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case uint value:
                    {
                        Converter<uint>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case long value:
                    {
                        Converter<long>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case ulong value:
                    {
                        Converter<ulong>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case float value:
                    {
                        Converter<float>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case double value:
                    {
                        Converter<double>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case decimal value:
                    {
                        Converter<decimal>.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }

                case DateTime value:
                    {
                        DateTimeConverter.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case string value:
                    {
                        StringConverter.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }
                case byte[] value:
                    {
                        ByteArrayConverter.GetConverter(handlerType).Write(value, byteBlock, size);
                        return;
                    }

                default:
                    {
                        throw new Exception("未定义的枚举类型：" + type.ToString());
                    }
            }
        }

        #endregion Serialize

        #region Deserialize

        public static T DeserializeObject<T>(byte[] data) where T : new()
        {
            if (data == null || !data.Any()) throw new ArgumentNullException(nameof(data));
            var type = typeof(T);
            var attr = GetContractAttr(type);
            var serializerObject = SerializerContext.GetSerializeObject(type);
            var byteBlock = new ByteBlock(data, attr.EndianType);
            return (T)DeserializeObject(byteBlock, serializerObject);
        }

        private static object DeserializeObject(ByteBlock byteBlock, SerializerObject serializerObject)
        {
            var instance = SerializerContext.GetNewInstance(serializerObject.Type);
            foreach (var property in serializerObject.Properties)
            {
                if (property.TypeCode == FastTypeCode.Class)
                {
                    var thisSerializerObject = SerializerContext.GetSerializeObject(property.Type);
                    var value = DeserializeObject(byteBlock, thisSerializerObject);
                    property.SetValue(ref instance, value);
                }
                else if (property.TypeCode == FastTypeCode.List)
                {
                    var value = DeserializeList(byteBlock, property);
                    property.SetValue(ref instance, value);
                }
                else
                {
                    var value = Deserialize(byteBlock, property.TypeCode, property.Size, property.TypeHandler);
                    property.SetValue(ref instance, value);
                }
            }
            return instance;
        }

        private static object DeserializeList(ByteBlock byteBlock, FastProperty property)
        {
            var instance = SerializerContext.GetNewInstance(property.Type);
            var size = property.HasLengthPlace ? byteBlock.ReadInt32(property.LengthPlaceSize) : property.Size;
            if (property.ArgTypeCode == FastTypeCode.Class)
            {
                var thisSerializerObject = SerializerContext.GetSerializeObject(property.ArgType);

                for (var i = 0; i < size; i++)
                {
                    var val = DeserializeObject(byteBlock, thisSerializerObject);
                    property.AddMethod.Invoke(instance, new object[] { val });
                }
            }
            else
            {
                for (var i = 0; i < size; i++)
                {
                    var val = Deserialize(byteBlock, property.ArgTypeCode, property.ArgSize, property.TypeHandler);
                    property.AddMethod.Invoke(instance, new object[] { val });
                }
            }

            return instance;
        }

        private static object Deserialize(ByteBlock byteBlock, FastTypeCode typeCode, int size, Type handlerType)
        {
            switch (typeCode)
            {
                case FastTypeCode.Boolean:
                    return Converter<bool>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Byte:
                    return Converter<byte>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Int16:
                    return Converter<short>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Int32:
                    return Converter<int>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Int64:
                    return Converter<long>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.UInt16:
                    return Converter<ushort>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.UInt32:
                    return Converter<uint>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.UInt64:
                    return Converter<ulong>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Double:
                    return Converter<double>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Float:
                    return Converter<float>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.Decimal:
                    return Converter<decimal>.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.DateTime:
                    return DateTimeConverter.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.String:
                    return StringConverter.GetConverter(handlerType).Read(byteBlock, size);

                case FastTypeCode.ByteArray:
                    return ByteArrayConverter.GetConverter(handlerType).Read(byteBlock, size);

                default:
                    throw new Exception("未定义的枚举类型：" + typeCode.ToString());
            }
        }

        #endregion Deserialize

        #region 辅助方法

        private static FastContractAttribute GetContractAttr(Type type)
        {
            var attr = type.GetCustomAttribute<FastContractAttribute>();
            if (attr == null)
            {
                throw new ArgumentNullException("类型为定义FastContractAttribute " + type.ToString());
            }

            return attr;
        }

        #endregion 辅助方法
    }
}