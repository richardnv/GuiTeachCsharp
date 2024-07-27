using System;
using System.Text.Json;
using JsonSer = System.Text.Json.JsonSerializer;

namespace GuiTeach;
public class GuitarNeck
{
    private GuitarString[] guitarStrings;
    private Tuning DefaultTuning = new Tuning();
    private int DefaultNumberOfFrets = 24;

    // Overloaded constructors
    /// <summary>
    /// Creates a GuitarNeck with the passed tuning and number of frets.
    /// String count is determined by the number of MidiNumbers in the tuning.
    /// </summary>
    /// <param name="tuning"></param>
    /// <param name="numberOfFrets"></param>
    public GuitarNeck(Tuning tuning, int numberOfFrets)
    {
        guitarStrings = new GuitarString[tuning.MidiNumbers.Length];
        for (int i = 0; i < tuning.MidiNumbers.Length; i++)
        {
            guitarStrings[i] = new GuitarString(tuning.MidiNumbers[i], i, numberOfFrets);
        }
    }

    /// <summary>
    /// Creates a GuitarNeck with the Default Tuning
    /// and the passed number of frets.
    /// </summary>
    /// <param name="numberOfFrets"></param>
    public GuitarNeck(int numberOfFrets)
    {        
        guitarStrings = new GuitarString[DefaultTuning.MidiNumbers.Length];
        for (int i = 0; i < DefaultTuning.MidiNumbers.Length; i++)
        {
            guitarStrings[i] = new GuitarString(DefaultTuning.MidiNumbers[i], i, numberOfFrets);
        }
    }
    
    /// <summary>
    /// Creates a GuitarNeck with the passed Tuning
    /// and the Default number of frets.
    /// </summary>
    /// <param name="tuning"></param>
    public GuitarNeck(Tuning tuning)
    {        
        guitarStrings = new GuitarString[tuning.MidiNumbers.Length];
        for (int i = 0; i < tuning.MidiNumbers.Length; i++)
        {
            guitarStrings[i] = new GuitarString(tuning.MidiNumbers[i], i, DefaultNumberOfFrets);
        }
    }

    /// <summary>
    /// Creates a GuitarNeck with the Default Tuning
    /// and the Default number of frets.
    /// </summary>
    public GuitarNeck()
    {        
        Tuning DefaultTuning = new Tuning();
        guitarStrings = new GuitarString[DefaultTuning.MidiNumbers.Length];
        for (int i = 0; i < DefaultTuning.MidiNumbers.Length; i++)
        {
            guitarStrings[i] = new GuitarString(DefaultTuning.MidiNumbers[i], i, DefaultNumberOfFrets);
        }
    }

    public Fingering FindClosestFingering(int targetMidiNumber, Fingering currentFingering)
    {
        // Assuming the method starts here...
        if (targetMidiNumber < 0 || targetMidiNumber > 127)
        {
            throw new NotAValidNoteNumberMidiException(targetMidiNumber);
        }
        if (currentFingering.StringIndex < 0 || currentFingering.StringIndex >= guitarStrings.Length)
        {
            throw new NotAValidFingeringStringException(currentFingering, guitarStrings.Length);
        }
        if (currentFingering.FretNumber < 0 || currentFingering.FretNumber > guitarStrings[currentFingering.StringIndex].NumberOfFrets)
        {
            throw new NotAValidFingeringFretException(currentFingering, guitarStrings[currentFingering.StringIndex].NumberOfFrets);
        }

        int minDistance = int.MaxValue;        
        Fingering closestFingering = new Fingering(0, 0); // Initialize with default values

        for (int s = 0; s < guitarStrings.Length; s++)
        {
            for (int fret = 0; fret <= guitarStrings[s].NumberOfFrets; fret++)
            {
                if (guitarStrings[s].Frets[fret].Note.MidiNumber == targetMidiNumber)
                {                       
                    int measuredDistance = CalculateDistance(currentFingering, new Fingering(s, fret));
                    if (measuredDistance < minDistance)
                    {
                        minDistance = measuredDistance;
                        closestFingering = new Fingering(s, fret);
                    }
                }             
            }
        }

        return closestFingering;
    }

    /// <summary>
    /// Converts a Fingering to a MidiNote.
    /// </summary>
    /// <param name="fingering">The fingering to convert.</param>
    /// <returns>The corresponding MidiNote.</returns>
    /// <exception cref="NotAValidFingeringStringException">Thrown when the string index is invalid.</exception>
    /// <exception cref="NotAValidFingeringFretException">Thrown when the fret number is invalid.</exception>
    public MidiNote FingeringToMidiNote(Fingering fingering)
    {
        const int MinIndex = 0;
        const int MinFretNumber = 0;

        if (fingering.StringIndex < MinIndex || fingering.StringIndex >= guitarStrings.Length - 1)
        {
            throw new NotAValidFingeringStringException(fingering, guitarStrings.Length);
        }

        if (fingering.FretNumber < MinFretNumber || fingering.FretNumber > guitarStrings[fingering.StringIndex].NumberOfFrets)
        {
            throw new NotAValidFingeringFretException(fingering, guitarStrings[fingering.StringIndex].NumberOfFrets);
        }

        return guitarStrings[fingering.StringIndex].Frets[fingering.FretNumber].Note;
    }

    public string MidiNumberToNoteSpelling(int midiNumber)
    {
        if (midiNumber < 0 || midiNumber > 127)
        {
            throw new NotAValidNoteNumberMidiException(midiNumber);
        }

        return new MidiNote(midiNumber).NoteSpelling;
    }

    private int CalculateDistance(Fingering currentFingering, Fingering newFingering)
    {        
        int strDif = Math.Abs(currentFingering.StringIndex - newFingering.StringIndex);
        int fretDif = Math.Abs(currentFingering.FretNumber - newFingering.FretNumber);
        return strDif + fretDif;
    }

}

public class NotAValidFingeringStringException : Exception
{
    public NotAValidFingeringStringException(Fingering fingering, int maxStrings)            
        : base($"Invalid string index {fingering.StringIndex}. Must be between 0 and {maxStrings - 1}.") { }

}

public class NotAValidFingeringFretException : Exception
{
    public NotAValidFingeringFretException(Fingering fingering, int maxFrets)            
        : base($"Invalid fret number {fingering.FretNumber}. Must be between 0 and {maxFrets}.") { }
}

public class NotAValidNoteNumberMidiException : Exception
{
    public NotAValidNoteNumberMidiException(int midiNumber)       
        : base($"Invalid MIDI note number {midiNumber}. Must be between 0 and 127.") { }
}
