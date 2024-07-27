using System.ComponentModel;

namespace GuiTeach;
public class Fingering : IEquatable<Fingering>
{
    public int StringIndex { get; }
    public int FretNumber { get; }
    public int StringNumber => StringIndex + 1;

    public Fingering(int? stringIndex = 0, int? fretNumber = 0)
    {
        StringIndex = stringIndex ?? 0;
        FretNumber = fretNumber ?? 0;
    }

    public string StringOrdinal()
    {        
        return (StringIndex + 1).ToOrdinal();        
    }

    public string FretOrdinal()
    {   
        if (FretNumber == 0)  { 
            return "Open";
        }
        return FretNumber.ToOrdinal();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FretNumber, StringIndex);
    }

    public bool Equals(Fingering? other)
    {
        if (other == null)
        {
            return false;
        }

        return FretNumber == other.FretNumber && StringIndex == other.StringIndex;
    }
}
