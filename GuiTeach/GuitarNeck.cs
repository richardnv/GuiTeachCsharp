using System;
using System.Text.Json;
using JsonSer = System.Text.Json.JsonSerializer;

namespace GuiTeach;

public class GuitarNeck
{
    public GuitarString[] guitarStrings;
        
    public GuitarNeck(int numberOfFrets = 24, Tuning? tuning = null)
    {        
        if (tuning == null)
        {
            tuning = new Tuning();
        }       
    
        if (numberOfFrets < 1 || numberOfFrets > 36)
        {
            throw new Exception("Number of frets must be between 1 and 36.");
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

    public int[] GetTuning()
    {
        int[] result = new int[guitarStrings.Length]; 
        foreach (var guitarString in guitarStrings)
        {
            result.Append(guitarString.Frets[0].Note.MidiNumber);                        
        }        
        return result;
    }

    public string MidiNumberToNoteName(int midiNumber)
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        return notes[midiNumber % 12];
    }

    public string MidiNumberToNoteSpelling(int midiNumber)
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        var result = notes[midiNumber % 12] + (midiNumber / 12 - 1);
        return result;
    }

    public Fingering FindClosestFingering(int noteNumber, Fingering currentFingering)
    {
        // Assuming 'currentFingering' is defined and represents the current position
        Fingering closestFingering = new Fingering(1, 0); // Replace null with a default Fingering object
        int minDistance = int.MaxValue; // Initialize with the maximum possible value

        if (currentFingering.String < 1 || currentFingering.String > guitarStrings.Length)
        {
            throw new NotAValidFingeringStringException(currentFingering, guitarStrings.Length);
        }

        if (currentFingering.Fret < 0 || currentFingering.Fret > guitarStrings[guitarStrings.Length - 1].NumberOfFrets)
        {
            throw new NotAValidFingeringFretException(currentFingering, guitarStrings[currentFingering.String-1].NumberOfFrets);
        }

        // Is the target note number within the Midi range?
        if (noteNumber < 0 || noteNumber > 127)
        {
            throw new NotAValidNoteNumberMidiException(noteNumber);
        } 

        // Is the target note number within the range of the current string configuration?
        if (noteNumber < guitarStrings[0].Frets[0].Note.MidiNumber || 
            noteNumber > guitarStrings[guitarStrings.Length - 1].Frets[guitarStrings[guitarStrings.Length - 1].NumberOfFrets].Note.MidiNumber)
        {
            throw new NotAValidNoteNumberStringConfigException(noteNumber, currentFingering.String, 
                guitarStrings[0].Frets[0].Note.MidiNumber, 
                guitarStrings[guitarStrings.Length -1].Frets[guitarStrings[guitarStrings.Length -1]
                    .NumberOfFrets].Note.MidiNumber);
        }

        for (int s = 0; s < guitarStrings.Length; s++)
        {

            for (int fret = 0; fret <= guitarStrings[s].NumberOfFrets; fret++)
            {
                if (guitarStrings[s].Frets[fret].Note.MidiNumber == noteNumber)
                {
                    int distance = Math.Abs(currentFingering.String - s) + Math.Abs(currentFingering.Fret - fret);
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

    public MidiNote FingeringToMidiNote(Fingering fingering)
    {
        if (fingering.String < 1 || fingering.String > guitarStrings.Length)
        {
            throw new NotAValidFingeringStringException(fingering, guitarStrings.Length);
        }

        if (fingering.Fret < 0 || fingering.Fret > guitarStrings[guitarStrings.Length - 1].NumberOfFrets)
        {
            throw new NotAValidFingeringFretException(fingering, guitarStrings[guitarStrings.Length - 1].NumberOfFrets);
        }

        return guitarStrings[fingering.String - 1].Frets[fingering.Fret].Note;
    }

    // Custom Exceptions for Geuitar Neck
    public class NotAValidFingeringStringException : Exception
    {        
        public NotAValidFingeringStringException(Fingering fingering, int guitarStringsLength)
            : base("Not a valid Fingering. (String out of range)")
        {
            Source = "GuitarNeck";
            Data.Add("String", fingering.String);
            Data.Add("StringRange: ", $"1 - {guitarStringsLength}");
        }        
    }

    public class NotAValidFingeringFretException : Exception 
    {
        public NotAValidFingeringFretException(Fingering fingering, int guitarStringsFretCount)
        {
            Exception ex = new Exception("Not a valid Fingering. (Fret out of range)")
            {
                Source = "GuitarNeck"
            };
            ex.Data.Add("Fret", fingering.Fret);
            ex.Data.Add("FretRange: ", $"0 - {guitarStringsFretCount}");
        }       
    }

    public class NotAValidNoteNumberMidiException : Exception 
    {
        public NotAValidNoteNumberMidiException(int noteNumber)
        {
            Exception ex = new Exception("Not a valid Note Number in Midi.")
            {
                Source = "GuitarNeck"
            };
            ex.Data.Add("NoteNumber", noteNumber);
            ex.Data.Add("NoteNumberRange: ", "0 - 127");
        }       
    }

    public class NotAValidNoteNumberStringConfigException : Exception 
    {
        public NotAValidNoteNumberStringConfigException(int noteNumber, int stringNumber, int minPlayableNoteNumber, int maxPlayableNoteNumber)
        {
            Exception ex = new Exception("Not a valid Note Number in String Configuration.")
            {
                Source = "GuitarNeck"
            };
            ex.Data.Add("NoteNumber", noteNumber);
            ex.Data.Add("StringNumber", stringNumber);
            ex.Data.Add("NoteNumberRange: ", $"{minPlayableNoteNumber} - {maxPlayableNoteNumber}");
        }        
    }
}




