// See https://aka.ms/new-console-template for more information

using System;

var guitarNeck = new GuitarNeck();

while (true)
{
    Console.Write("Enter a midi number between 0 and 127: ");
    var midiNumber = Convert.ToInt32(Console.ReadLine());

    if (midiNumber < 0 || midiNumber > 127)
    {
        Console.WriteLine("Midi number must be between 0 and 127. Goodbye!");
        break;    
    }

    Console.WriteLine($"The note spelling for that midi number {midiNumber} is {guitarNeck.MidiNumberToNoteSpelling(midiNumber)}.");

    var curFingering = new Fingering(1, 0);
    var newFingering = new Fingering(1, 0);
    try {
        newFingering = guitarNeck.FindClosestFingering(midiNumber, curFingering);
        Console.WriteLine($"The closest string and fret where this note can be played on a std tuning guitar at String: {newFingering.String}, Fret: {newFingering.Fret}.");
    } catch (Exception e) {
        Console.WriteLine(e.Message);            
    }
    
    
}
