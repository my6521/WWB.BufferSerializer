namespace WWB.BufferSerializer.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FastPropertyAttribute : Attribute
    {
        private int _order;
        private int _size;
        private bool _isIgnore = false;
        private int _argSize = 1;
        private Type _handlerType;
        private int _lengthPlaceSize = 1;
        private Type _typeHandler;

        public FastPropertyAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order">排序</param>
        public FastPropertyAttribute(int order)
        {
            _order = order;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order => _order;

        /// <summary>
        /// 字段占用的字节长度
        /// 支持Int32、String、List、自定义TypeHandler；
        /// 如果为0并且也不是自动转换类型，则该字段包的首字节认为是长度，
        /// </summary>
        public int Size
        {
            get { return _size; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Size不能小于1");
                }
                _size = value;
            }
        }

        /// <summary>
        /// 忽略字段
        /// </summary>
        public bool IsIgnore
        {
            get { return _isIgnore; }
            set { _isIgnore = value; }
        }

        public int ArgSize
        {
            get { return _argSize; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("ArgSize不能小于1");
                }
                _argSize = value;
            }
        }

        /// <summary>
        /// 自定义处理器
        /// </summary>
        public Type HandlerType
        {
            get { return _handlerType; }
            set { _handlerType = value; }
        }

        /// <summary>
        /// 不定长字段长度占位字节数
        /// </summary>
        public int LengthPlaceSize
        {
            get { return _lengthPlaceSize; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("FieldLengthSize不能小于1");
                }
                _lengthPlaceSize = value;
            }
        }

        public Type TypeHandler
        {
            get { return _typeHandler; }
            set { _typeHandler = value; }
        }
    }
}