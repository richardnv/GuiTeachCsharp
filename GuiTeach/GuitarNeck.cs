using System;
using System.Linq;

class GuitarNeck
{
    private GuitarString[] guitarStrings;
    
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
        guitarStrings = tuning.MidiNumbers.Select(mn => new GuitarString(mn, numberOfFrets)).ToArray();
        
    }

    public string MidiNumberToNoteName(int midiNumber)
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        return notes[midiNumber % 12];
    }

   /* public string GetNoteAtStringAndFret(int gstring, int fret)
    {
        if (gstring < 1 || gstring > 6)
        {
            throw new Exception("Invalid string number. Must be between 1 and 6.");
        }

        return strings[gstring - 1].Frets[fret].Note;
    } */

    /* 
    public (int closestString, int closestFret) FindClosestStringAndFret(int startMidiNumber, int targetMidiNumber)
    {
        var closestDistance = int.MaxValue;
        var closestString = -1;
        var closestFret = -1;

        foreach (var (gstring, stringIndex) in strings.Select((gstring, index) => (gstring, index)))
        {
            var fretDistance = targetMidiNumber - gstring.OpenNoteMidiNumber;
            if (fretDistance >= 0 && fretDistance < closestDistance)
            {
                var startFretDistance = startMidiNumber - gstring.OpenNoteMidiNumber;
                if (startFretDistance <= fretDistance)
                {
                    closestDistance = fretDistance;
                    closestString = stringIndex + 1; // Adjusting index to match string numbering
                    closestFret = fretDistance;
                }
            }
        }

        return (closestString, closestFret);
    }    */
}

public class GuitarString
{
    public GuitarFret[] Frets { get; }
    public int NumberOfFrets => Frets.Length - 1;

    public GuitarString(int midiNumber, int numberOfFrets = 24)
    {       
        if (midiNumber < 0 || midiNumber > 127)
        {
            throw new Exception("Midi number must be between 0 and 127.");
        }

        try 
        {
            Frets = new GuitarFret[numberOfFrets + 1];
            Frets[0] = new GuitarFret(midiNumber);
            for (var i = 1; i < numberOfFrets + 1; i++)
            {
                Frets[i] = new GuitarFret(midiNumber + i);
            }

        } catch (Exception e) {
            Console.WriteLine("MidiNumber + NumberOfFrets exceeds 129: " + e.Message);
            throw new Exception("MidiNumber + NumberOfFrets exceeds 129: " + e.Message);
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

