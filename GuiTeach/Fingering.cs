using System.ComponentModel;

namespace GuiTeach;
public class Fingering
{
    public int StringNumber { get; }

    public int FretNumber { get; }

    public Fingering(int? stringNumber = 1, int? fretNumber = 0)
    {
        StringNumber = stringNumber ?? 1;
        FretNumber = fretNumber ?? 0;
    }

    public string GetStringOrdinal()
    {
        if (StringNumber < 1)
        {
            throw new InvalidEnumArgumentException("StringNumber must be >= 1.");
        }

        string suffix = "th";

        if (StringNumber < 20)
        {
            suffix = StringNumber switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        else if (StringNumber <= 100 && StringNumber % 10 < 4)
        {
            suffix = (StringNumber % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        else if (StringNumber % 100 < 20 && StringNumber > 100)
        {
            suffix = "th";
        }
        else if (StringNumber % 10 < 4)
        {
            suffix = (StringNumber % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        
        
        return $"{StringNumber}{suffix}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Fingering other = (Fingering)obj;
        return FretNumber == other.FretNumber && StringNumber == other.StringNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FretNumber, StringNumber);
    }
}