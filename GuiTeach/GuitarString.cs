namespace GuiTeach
{
    public class GuitarString
    {
        // Properties
        public int StringIndex { get; }    
        public GuitarFret[] Frets { get; }
        public int NumberOfFrets => Frets.Length - 1;
        public string OpenNoteSpelling => Frets[0].Note.NoteSpelling;
        public int StringNumber => StringIndex + 1;

        // Overloaded constructors
        public GuitarString(int midiNumber, int stringIndex, int numberOfFrets)
        {       
            StringIndex = stringIndex;

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
            StringIndex = 0;

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
            StringIndex = 0;        
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

        /// <summary>
        /// Returns the string number as an ordinal string.
        /// will never return "0th".
        /// </summary>
        /// <returns></returns>
        public string StringOrdinal()
        {
            return StringNumber.ToOrdinal();
        }
    }
}