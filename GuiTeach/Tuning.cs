namespace GuiTeach;

public class Tuning
{
    public string Name { get; } 
    public int[] MidiNumbers { get; }

    public Tuning(string name = "Standard", int[]? midiNumbers = null)
    {        
        MidiNumbers = new int[6];        
        Name = name;
        if (midiNumbers == null)
        {
            Name = "Standard";
            MidiNumbers = new int[] { 40, 45, 50, 55, 59, 64 };
        }   
        else 
        {
            MidiNumbers = midiNumbers;                      
        }        
    }
}