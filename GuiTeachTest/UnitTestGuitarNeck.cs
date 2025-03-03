using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using GuiTeach;

namespace GuiTeachTest;

public class UnitTestGuitarNeck
{
    [Fact]
    public void Test_Can_create_standard_Tuning()
    {
        var tuning = new Tuning();
        Assert.Equal("Standard", tuning.Name);
    }

    [Fact]
    public void Test_Can_create_std_Bass_Tuning()
    {
        var tuning = new Tuning("Bass Standard", [28, 33, 38, 43]);
        Assert.Equal("Bass Standard", tuning.Name);
        Assert.Equal([28, 33, 38, 43], tuning.MidiNumbers);
    }

    [Fact]
    public void Test_create_DropD_gtr_Tuning()
    {
        var midiNumbers = new[] { 38, 45, 50, 55, 59, 64 };
        var tuning = new Tuning("Drop D", midiNumbers);
        Assert.Equal("Drop D", tuning.Name);
        Assert.Equal(midiNumbers, tuning.MidiNumbers);
    }

    [Fact]
    public void Test_create_std_7str_gtr_Tuning()
    {
        var midiNumbers = new[] { 35, 40, 45, 50, 55, 59, 64 };
        var tuning = new Tuning("Standard 7str", midiNumbers);
        Assert.Equal("Standard 7str", tuning.Name);
        Assert.Equal(midiNumbers, tuning.MidiNumbers);
    }

    [Fact]
    public void Test_create_std_8str_gtr_Tuning()
    {
        var midiNumbers = new[] { 31, 35, 40, 45, 50, 55, 59, 64 };
        var tuning = new Tuning("Standard 8str", midiNumbers);
        Assert.Equal("Standard 8str", tuning.Name);
        Assert.Equal(midiNumbers, tuning.MidiNumbers);
    }

    [Fact]
    public void Test_create_std_GuitarString()
    {
        var guitarString = new GuitarString(40,0,24);
        Assert.Equal(24, guitarString.NumberOfFrets);
        Assert.Equal(40, guitarString.Frets[0].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[0].Note.NoteName);
        Assert.Equal(2, guitarString.Frets[0].Note.Octave);
        Assert.Equal(64, guitarString.Frets[24].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[24].Note.NoteName);
        Assert.Equal(4, guitarString.Frets[24].Note.Octave);
    }

    [Fact]
    public void Test_create_std_BassString()
    {
        var guitarString = new GuitarString(28,0,24);
        Assert.Equal(24, guitarString.NumberOfFrets);
        Assert.Equal(28, guitarString.Frets[0].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[0].Note.NoteName);
        Assert.Equal(1, guitarString.Frets[0].Note.Octave);
        Assert.Equal(52, guitarString.Frets[24].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[24].Note.NoteName);
        Assert.Equal(3, guitarString.Frets[24].Note.Octave);
    }

    [Fact]
    public void Test_NoteInfo_from_MidiNumber()
    {
        var targetMNN = 40;
        var midiNote = new MidiNote(targetMNN);
        Assert.Equal("E", midiNote.NoteName);
        Assert.Equal(targetMNN, midiNote.MidiNumber);
        Assert.Equal(2, midiNote.Octave);
    }

    [Fact]
    public void Test_disallow_settings_exceeding_MidiBounds()
    {     
        Assert.ThrowsAny<Exception>(() => new GuitarString(128));
        Assert.ThrowsAny<Exception>(() => new GuitarString(104, 24)); 
        Assert.ThrowsAny<Exception>(() => new GuitarString(100, 28)); 
    }
    
    [Fact]
    public void Test_find_closest_fingering()
    {
        var guitarNeck = new GuitarNeck();

        // Test current fingering lower than target note in same position - Small change
        Assert.Equal(new Fingering(1,3), guitarNeck.FindClosestFingering(48, new Fingering(0,0)));

        // Same position - Large change
        //      Test current fingering is on a lower string and same fret of the target note on a 
        //      higher string
        Assert.Equal(new Fingering(4,15), guitarNeck.FindClosestFingering(74, new Fingering(0,15)));        
        //      Test current fingering is on a lower string and fret in the same position as 
        //      the target note on a higher string
        Assert.Equal(new Fingering(4,15), guitarNeck.FindClosestFingering(74, new Fingering(1,14)));
        //      Test current fingering is on a higher string and fret in the same position as 
        //      the target note on a lower string - Small change
        Assert.Equal(new Fingering(1,10), guitarNeck.FindClosestFingering(55, new Fingering(4,11)));
    }

    [Fact]
    public void Test_fingering_to_MidiNote()
    {
        var guitarNeck = new GuitarNeck();  // Standard tuning - 24 frets
        Assert.Equal(new MidiNote(40), guitarNeck.FingeringToMidiNote(new Fingering(0,0)));    
        Assert.Equal(new MidiNote(88), guitarNeck.FingeringToMidiNote(new Fingering(5,24)));     
        Assert.Equal(new MidiNote(71), guitarNeck.FingeringToMidiNote(new Fingering(4,12)));     
        guitarNeck = new GuitarNeck(12);    // Standard tuning - only 12 frets
        Assert.Equal(new MidiNote(76), guitarNeck.FingeringToMidiNote(new Fingering(5,12)));
        guitarNeck = new GuitarNeck(36);    // Standard tuning - 36 frets
        Assert.Equal(new MidiNote(91), guitarNeck.FingeringToMidiNote(new Fingering(3,36)));
        Assert.Equal(new MidiNote(100), guitarNeck.FingeringToMidiNote(new Fingering(5,36)));    
        guitarNeck = new GuitarNeck(new Tuning("Bass Standard",[28,33,38,43]));    // Bass Standard tuning - 4 String - 24 frets
        Assert.Equal(new MidiNote(28), guitarNeck.FingeringToMidiNote(new Fingering(0,0)));
        Assert.Equal(new MidiNote(41), guitarNeck.FingeringToMidiNote(new Fingering(2,3)));
        Assert.Equal(new MidiNote(67), guitarNeck.FingeringToMidiNote(new Fingering(3,24))); 
        guitarNeck = new GuitarNeck(new Tuning("Drop D",[38,45,50,55,59,64]));    // Drop D tuning - 24 frets
        Assert.Equal(new MidiNote(38), guitarNeck.FingeringToMidiNote(new Fingering(0,0)));
        Assert.Equal(new MidiNote(88), guitarNeck.FingeringToMidiNote(new Fingering(5,24)));       
        guitarNeck = new GuitarNeck(new Tuning("7str Standard", [35, 40, 45, 50, 55, 59, 64]));  // Standard 7 string tuning - 24 frets
        Assert.Equal(new MidiNote(35), guitarNeck.FingeringToMidiNote(new Fingering(0,0)));              
        Assert.Equal(new MidiNote(88), guitarNeck.FingeringToMidiNote(new Fingering(6,24)));  
        guitarNeck = new GuitarNeck(new Tuning("Funky", [26, 31, 36, 41, 46, 51, 56, 61, 66, 71, 76]),15);  // Alt Tuning-11String-15Frets
        Assert.Equal(new MidiNote(26), guitarNeck.FingeringToMidiNote(new Fingering(0,0)));
        Assert.Equal(new MidiNote(51), guitarNeck.FingeringToMidiNote(new Fingering(4,5)));
        Assert.Equal(new MidiNote(76), guitarNeck.FingeringToMidiNote(new Fingering(10,0)));
        Assert.Equal(new MidiNote(91), guitarNeck.FingeringToMidiNote(new Fingering(10,15)));
    }
    
    
    [Fact]
    public void Test_find_closest_fingering_invalid_target_note_Midi_Exception()
    {
        var guitarNeck = new GuitarNeck();        
        Assert.Throws<NotAValidNoteNumberMidiException>(() => guitarNeck.FindClosestFingering(128, new Fingering(0,0)));      
    }

    [Fact]
    public void Test_find_closest_fingering_current_fingering_string_Exception()
    {
        var guitarNeck = new GuitarNeck();        
        Assert.Throws<NotAValidFingeringStringException>(() => guitarNeck.FindClosestFingering(40, new Fingering(-1,0)));      
        Assert.Throws<NotAValidFingeringStringException>(() => guitarNeck.FindClosestFingering(40, new Fingering(6,0)));
    }

    [Fact]
    public void Test_find_closest_fingering_current_fingering_fret_Exception()
    {
        var guitarNeck = new GuitarNeck();        
        Assert.Throws<NotAValidFingeringFretException>(() => guitarNeck.FindClosestFingering(40, new Fingering(1,-1)));      
        Assert.Throws<NotAValidFingeringFretException>(() => guitarNeck.FindClosestFingering(40, new Fingering(1,45)));      
    }
    
    [Fact]
    public void Test_Fingering_StringOrdinal()
    {
        var fingering = new Fingering(0,0);
        Assert.Equal("1st", fingering.StringOrdinal());
        fingering = new Fingering(1,0);
        Assert.Equal("2nd", fingering.StringOrdinal());
        fingering = new Fingering(2,0);
        Assert.Equal("3rd", fingering.StringOrdinal());
        fingering = new Fingering(3,0);
        Assert.Equal("4th", fingering.StringOrdinal());
        fingering = new Fingering(4,0);
        Assert.Equal("5th", fingering.StringOrdinal());
        fingering = new Fingering(10,0);
        Assert.Equal("11th", fingering.StringOrdinal());
        fingering = new Fingering(20,0);
        Assert.Equal("21st", fingering.StringOrdinal());        
        fingering = new Fingering(31,0);
        Assert.Equal("32nd", fingering.StringOrdinal());        
        fingering = new Fingering(122,0);
        Assert.Equal("123rd", fingering.StringOrdinal());
        fingering = new Fingering(83,0);
        Assert.Equal("84th", fingering.StringOrdinal());
    }
}