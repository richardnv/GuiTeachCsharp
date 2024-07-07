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

    Console.WriteLine($"The note name for midi number {midiNumber} is {guitarNeck.MidiNumberToNoteName(midiNumber)}.");
    Console.WriteLine($"The closest string and fret for midi number {midiNumber} is {guitarNeck.FindClosestStringAndFret(midiNumber, 0, 3)}.");
}
