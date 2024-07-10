namespace GuiTeach;
public class GuitarFret
{
    public MidiNote Note { get; }
    
    public GuitarFret(int midiNumber)
    {
        if (midiNumber < 0 || midiNumber > 127)
        {
            throw new Exception("Midi number must be between 0 and 127.");
        }
        Note = new MidiNote(midiNumber);
    }    
}
