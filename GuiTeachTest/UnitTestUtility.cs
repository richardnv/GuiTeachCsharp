using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using GuiTeach;

namespace GuiTeachTest;

public class UnitTestUtility
{
    [Fact]
    public void Test_Utility_Get_Ordinal()
    {
        Assert.Equal("1st", Utility.Get_Ordinal(1));
        Assert.Equal("2nd", Utility.Get_Ordinal(2));
        Assert.Equal("3rd", Utility.Get_Ordinal(3));
        Assert.Equal("4th", Utility.Get_Ordinal(4));
        Assert.Equal("5th", Utility.Get_Ordinal(5));
        Assert.Equal("10th", Utility.Get_Ordinal(10));
        Assert.Equal("11th", Utility.Get_Ordinal(11));
        Assert.Equal("12th", Utility.Get_Ordinal(12));
        Assert.Equal("13th", Utility.Get_Ordinal(13));
        Assert.Equal("14th", Utility.Get_Ordinal(14));
        Assert.Equal("20th", Utility.Get_Ordinal(20));
        Assert.Equal("21st", Utility.Get_Ordinal(21));
        Assert.Equal("22nd", Utility.Get_Ordinal(22));
        Assert.Equal("23rd", Utility.Get_Ordinal(23));
        Assert.Equal("24th", Utility.Get_Ordinal(24));
        Assert.Equal("30th", Utility.Get_Ordinal(30));
        Assert.Equal("31st", Utility.Get_Ordinal(31));
        Assert.Equal("32nd", Utility.Get_Ordinal(32));
        Assert.Equal("33rd", Utility.Get_Ordinal(33));
        Assert.Equal("34th", Utility.Get_Ordinal(34));
        Assert.Equal("111th", Utility.Get_Ordinal(111));
        Assert.Equal("123rd", Utility.Get_Ordinal(123));
    }
}
