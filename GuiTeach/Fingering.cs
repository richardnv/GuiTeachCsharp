namespace GuiTeach;
public class Fingering
{
    public int String { get; }

    public int Fret { get; }

    public Fingering(int? stringNumber = 1, int? fretNumber = 0)
    {
        String = stringNumber ?? 1;
        Fret = fretNumber ?? 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Fingering other = (Fingering)obj;
        return Fret == other.Fret && String == other.String;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Fret, String);
    }
}