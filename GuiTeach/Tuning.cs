namespace GuiTeach;

public class Tuning
{
    public string Name { get; } 
    public int[] MidiNumbers { get; }

    private int[] DefaultStandardTuning = [40, 45, 50, 55, 59, 64];


    // Overloaded constructor
    public Tuning(string name, int[] midiNumbers)
    {                
        Name = name;
        if (midiNumbers == null)
        {
            Name = "Standard";
            MidiNumbers = DefaultStandardTuning;
        }   
        else 
        {
            MidiNumbers = midiNumbers;                      
        }        
    }

    public Tuning()
    {
        Name = "Standard";
        MidiNumbers = DefaultStandardTuning;
    }
}