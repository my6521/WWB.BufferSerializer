using WWB.BufferSerializer.Data;

namespace WWB.BufferSerializer.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class FastContractAttribute : Attribute
    {
        private EndianType _endianType;
        private int _size = 512;

        public EndianType EndianType
        {
            get { return _endianType; }
            set { _endianType = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}