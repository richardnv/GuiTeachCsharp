GuiTeach is a study in two of my loves. Playing Guitar and Writing Code that does interesting things.

I have always kicked around the idea that there must be a way to determine the best place on the guitar neck to play a given note.
Seems simple right? 
  What if the not I am currently fingering the C on the A String at Fret 3 (Note spelling of C3 / C natural in the 3rd octave / midi note 48) 
and the next note I want to play is the A2 (midi note 45) which can be played on the open A string (same string I'm already on.)  
Still Easy Right?
  But what if the next note is a bend to A#2 (midi note 46). Kinda weird to bend an open string, can be done but 
it would be totaly easy to play the A2 on String 1 Fret 5 and then bend it to A#2.

So you see that the best place to play a note is dependant on what came before and what is comming up.

Well, I want to load a midi file, select a track and see how it can be played in real time. 
Taking into consideration: 
  1. Tuning
  2. Number of strings
  3. current position
  4. Number of fingers currently in use and where.
  5. Chords are going to be a bitch.
  6. Midi player will have to read ahead to have the info needed to calculate the best not.....
Well as of now I have the rudaments of a way to display this all on a web page useing a programaticly created SVG that is animated by JavaScript.

Standard 6 string with 24 frets.
![image](https://github.com/user-attachments/assets/9494d8ca-a8c9-4015-843b-137100eaf4ea)

zoomed in, less frets are visible.
![image](https://github.com/user-attachments/assets/0ecf230f-5033-4b26-840f-641852de95e3)

How about an 11 string guitar?
![image](https://github.com/user-attachments/assets/d0dffe74-14bb-4d22-b7f0-001a37c8a14a)
