using System.ComponentModel;

namespace GuiTeach;
public class Fingering : IEquatable<Fingering>
{
    public int StringNumber { get; }
    public int FretNumber { get; }

    public Fingering(int? stringNumber = 1, int? fretNumber = 0)
    {
        StringNumber = stringNumber ?? 1;
        FretNumber = fretNumber ?? 0;
    }

    public string StringOrdinal()
    {
        return Utility.Get_Ordinal(StringNumber);        
    }

    public string FretOrdeinal()
    {
        return Utility.Get_Ordinal(FretNumber);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FretNumber, StringNumber);
    }

    public bool Equals(Fingering? other)
    {
        if (other == null)
        {
            return false;
        }

        return FretNumber == other.FretNumber && StringNumber == other.StringNumber;
    }
}
