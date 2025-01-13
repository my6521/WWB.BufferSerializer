# wwb-bufferserializer

### demo
```
var obj = new TestClass1
{
    Num1 = 1,
    Num2 = 2,
    Num3 = 3,
    Num4 = 4,
    Num5 = 5,
    Num6 = 6,
    Num7 = 7,
    Num8 = 8,
    Num9 = 9,
    Num10 = 10,
    Str11 = "1.1",
    DateTime12 = DateTime.Now,
    List1 = new List<int> { 11, 12 },
    Class2 = new TestClass2
    {
        P1 = 0x0C,
        P2 = 0x0D,
    },
    List2 = new List<TestClass2>
    {
        new TestClass2{ P1 = 20,P2 =  21},
        new TestClass2{ P1 = 22,P2 =  23}
    },
    List3 = new List<int> { 100, 101 },
    List4 = new List<string> { "0102", "0304" }
};

var data = FastSerializer.SerializeObject<TestClass1>(obj);
var result = FastSerializer.DeserializeObject<TestClass1>(data);

[FastContract(EndianType = EndianType.Big)]
class TestClass1
{
    [FastProperty(1)]
    public byte Num1 { get; set; }

    [FastProperty(2)]
    public short Num2 { get; set; }

    [FastProperty(3)]
    public ushort Num3 { get; set; }

    [FastProperty(4)]
    public int Num4 { get; set; }

    [FastProperty(5)]
    public uint Num5 { get; set; }

    [FastProperty(6)]
    public long Num6 { get; set; }

    [FastProperty(7)]
    public ulong Num7 { get; set; }

    [FastProperty(8)]
    public double Num8 { get; set; }

    [FastProperty(9)]
    public float Num9 { get; set; }

    [FastProperty(10)]
    public decimal Num10 { get; set; }

    [FastProperty(11, Size = 3, TypeHandler = typeof(ASCIIConverter))]
    public string Str11 { get; set; }

    [FastProperty(12, TypeHandler = typeof(CP56Time2aConverter))]
    public DateTime DateTime12 { get; set; }

    [FastProperty(13, Size = 2, ArgSize = 1)]
    public List<int> List1 { get; set; }

    [FastProperty(14)]
    public TestClass2 Class2 { get; set; }

    [FastProperty(15, LengthPlaceSize = 2)]
    public List<TestClass2> List2 { get; set; }

    [FastProperty(16)]
    public List<int> List3 { get; set; }

    [FastProperty(17, ArgSize = 2)]
    public List<string> List4 { get; set; }
}

class TestClass2
{
    [FastProperty]
    public byte P1 { get; set; }

    [FastProperty]
    public short P2 { get; set; }
}
```