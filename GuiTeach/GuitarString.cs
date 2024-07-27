namespace GuiTeach;

public class GuitarString
{
    public int StringNumber { get; }    
    public GuitarFret[] Frets { get; }
    public int NumberOfFrets => Frets.Length - 1;
    public string OpenNoteSpelling => Frets[0].Note.NoteSpelling;

    // Overloaded constructors
    public GuitarString(int midiNumber, int stringNumber, int numberOfFrets)
    {       
        StringNumber = stringNumber;

        try 
        {
            Frets = new GuitarFret[numberOfFrets + 1];
            Frets[0] = new GuitarFret(midiNumber);
            for (var i = 1; i < numberOfFrets + 1; i++)
            {
                Frets[i] = new GuitarFret(midiNumber + i);
            }

        } catch (Exception e) {
            var msg = "MidiNumber + NumberOfFrets exceeds 129: " + e.Message;
            Console.WriteLine(msg);
            throw new Exception(msg);
        }        
    }    
    public GuitarString(int midiNumber, int numberOfFrets)
    {       
            StringNumber = 1;

            try 
            {
                Frets = new GuitarFret[numberOfFrets + 1];
                Frets[0] = new GuitarFret(midiNumber);
                for (var i = 1; i < numberOfFrets + 1; i++)
                {
                    Frets[i] = new GuitarFret(midiNumber + i);
                }

            } catch (Exception e) {
                var msg = "MidiNumber + NumberOfFrets exceeds 129: " + e.Message;
                Console.WriteLine(msg);
                throw new Exception(msg);
            }        
    }    
    public GuitarString(int midiNumber)
    {       
        StringNumber = 1;        
        var numberOfFrets = 24;

        try 
        {
            Frets = new GuitarFret[numberOfFrets + 1];
            Frets[0] = new GuitarFret(midiNumber);
            for (var i = 1; i < numberOfFrets + 1; i++)
            {
                Frets[i] = new GuitarFret(midiNumber + i);
            }

        } catch (Exception e) {
            var msg = "MidiNumber + NumberOfFrets exceeds 129: " + e.Message;
            Console.WriteLine(msg);
            throw new Exception(msg);
        }        
    }    

    public string StringOrdinal()
    {
        return Utility.Get_Ordinal(StringNumber);
    }
}