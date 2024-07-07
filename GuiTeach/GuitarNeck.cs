using System;
using System.Linq;

public class GuitarNeck
{
    public GuitarString[] guitarStrings;
        
    public GuitarNeck(int numberOfFrets = 24, Tuning? tuning = null)
    {        
        if (tuning == null)
        {
            tuning = new Tuning();
        }       
    
        if (numberOfFrets < 1 || numberOfFrets > 32)
        {
            throw new Exception("Number of frets must be between 1 and 32.");
        }
    
        guitarStrings = new GuitarString[tuning.MidiNumbers.Length]; // Initialize the guitarStrings field
    
        for (var i = 0; i < tuning.MidiNumbers.Length; i++)
        {
            if (tuning.MidiNumbers[i] < 0 || tuning.MidiNumbers[i] > 127)
            {
                throw new Exception("Midi number must be between 0 and 127.");
            }
            guitarStrings[i] = new GuitarString(tuning.MidiNumbers[i], i+1, numberOfFrets);
        }                
    }

    public string MidiNumberToNoteName(int midiNumber)
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        return notes[midiNumber % 12];
    }

    public Fingering FindClosestFingering(int noteNumber, Fingering currentFingering)
    {
        // Assuming 'currentFingering' is defined and represents the current position
        Fingering closestFingering = new Fingering(1, 0); // Replace null with a default Fingering object
        int minDistance = int.MaxValue; // Initialize with the maximum possible value

        // ToDo: Implement the logic to throw new Exception ("Not a valid Fingering")
        // Todo: Implement the logic to throw new Exception ("Not a valid Note Number")

        for (int s = 0; s < guitarStrings.Length; s++)
        {
            for (int fret = 0; fret < guitarStrings[s].NumberOfFrets; fret++)
            {
                if (guitarStrings[s].Frets[fret].Note.MidiNumber == noteNumber)
                {
                    int distance = Math.Abs(currentFingering.String - (s + 1)) + Math.Abs(currentFingering.Fret - fret);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestFingering = new Fingering(s + 1, fret);
                    }
                }
            }
        }

        return closestFingering;
    }
}

public class GuitarString
{
    public int StringNumber { get; }    
    public GuitarFret[] Frets { get; }
    public int NumberOfFrets => Frets.Length - 1;
    public string OpenNoteSpelling => Frets[0].Note.NoteSpelling;

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
}

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

public class MidiNote
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

    public int MidiNumberToOctave(int midiNumber)
    {
        return (midiNumber / 12) - 1;
    }
}

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

