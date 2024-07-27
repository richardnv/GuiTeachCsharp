namespace GuiTeach;

public class MidiNote : IEquatable<MidiNote>
{
    public int MidiNumber { get; }
    public string NoteName { get; }
    public int Octave { get; }
    public string NoteSpelling => $"{NoteName}{Octave}";

    public MidiNote(int midiNumber)
    {
        MidiNumber = midiNumber;
        NoteName = MidiNumberToNoteName(midiNumber);
        Octave = MidiNumberToOctave(midiNumber);
    }

    private string MidiNumberToNoteName(int midiNumber)
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        return notes[midiNumber % 12];
    }

    private int MidiNumberToOctave(int midiNumber)
    {
        return (midiNumber / 12) - 1;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MidiNumber, NoteName, Octave);
    }

    public bool Equals(MidiNote? other)
    {
        if (other == null)
        {
            return false;
        }

        return MidiNumber == other.MidiNumber;
    }
}
