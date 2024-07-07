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
        var guitarString = new GuitarString(40);
        Assert.Equal(24, guitarString.NumberOfFrets);
        Assert.Equal(40, guitarString.Frets[0].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[0].Note.NoteName);
        Assert.Equal(2, guitarString.Frets[0].Note.Octave);
        Assert.Equal(64, guitarString.Frets[24].Note.MidiNumber);
        Assert.Equal("E", guitarString.Frets[24].Note.NoteName);
        Assert.Equal(4, guitarString.Frets[24].Note.Octave);
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
    
}