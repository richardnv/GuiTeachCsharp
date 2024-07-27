using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using GuiTeach;

namespace GuiTeachTest;

public class UnitTestIntExtensions
{
    [Fact]
    public void Test_StringNumber_ToOrdinal()
    {
        Assert.Equal("1st", 1.ToOrdinal());
        
    }
}
