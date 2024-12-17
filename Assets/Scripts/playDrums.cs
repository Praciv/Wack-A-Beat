using System;
using System.Collections;
using System.Collections.Generic;
using MidiPlayerTK;
using UnityEngine;

public class playDrums : MonoBehaviour
{
    public static int songIndex { get; set; } 
    public static float speed { get; set; }
    public MidiFilePlayer midiFilePlayer; 
    private bool isPlaying = false; // track play status locally to sync with SongPlayer.cs
    private int targetChannel = 9;
    
    //finds the midifileplayer, loads the song specified by the songIndex variable and the speed 
    //adds the function to call when a midi note is played 
    void Start()
    {
        midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();

        if(midiFilePlayer == null)
        {
            Debug.LogError("MidiFilePlayer not found in the scene");
            return;
        }

        midiFilePlayer.MPTK_MidiIndex = songIndex;

        MidiLoad midiLoaded = midiFilePlayer.MPTK_Load();

        midiFilePlayer.MPTK_Speed = speed;
        
        midiFilePlayer.OnEventNotesMidi.AddListener(OnMidiEvent);   
    }

    //starts the midifileplayer and toggles the isPlaying variable to true
    public void StartDrums() {
        if (!isPlaying) {
            isPlaying = true; // toggle
            midiFilePlayer.MPTK_Play(alreadyLoaded: true);
            Debug.Log("Drums started.");
        }
    }

    //checks if the song is playing and pauses it
    public void PauseDrums() {
        if (isPlaying) {
            isPlaying = false; // toggle
            midiFilePlayer.MPTK_Pause();
            Debug.Log("Drums paused.");
        }
    }

    //checks if the song is playing and if it isn't resumes it
    public void ResumeDrums() {
        if (!isPlaying)
        {
            midiFilePlayer.MPTK_UnPause();
            isPlaying = true;
            Debug.Log("Drums resumed.");
        }
    }

    //checks if the song is playing and stops it if it is 
    public void StopDrums() {
        if (isPlaying)
        {
            midiFilePlayer.MPTK_Stop();
            isPlaying = false;
            Debug.Log("Drums stopped.");
        }
    }

    //called by the midi sequencer just before the notes are playing 
    // by the midi synthesisor 
    void OnMidiEvent(List<MPTKEvent> midiEvents)
    {
        //loops through all the midi events about to paly
        foreach (var midiEvent in midiEvents)
        {
            // Check if the event is on the target channel
            if (midiEvent.Channel == targetChannel)
            {
                //checks if the midi event is a note being played 
                if(midiEvent.Command == MPTKCommand.NoteOn && midiEvent.Velocity > 0)
                {
                    Debug.Log($"Channel: {midiEvent.Channel}, Note: {midiEvent.Value}, Velocity: {midiEvent.Velocity}, Time: {midiEvent.RealTime}");
                    Debug.Log($"Note duration: {midiEvent.Duration / 1000.0f}");
                 
                    //calls the activateSprite function with the drum name returned from the formatDrumValues function 
                    activateSprite(formatDrumValues(midiEvent.Value));
                }
            }
        }
    }

    //MIDI values: https://www.music.mcgill.ca/~ich/classes/mumt306/StandardMIDIfileformat.html#BMA1_3
    //takes in the value from the midiEvent and maps it to a drum name 
    //inline with the standard midi format specified above  
    String formatDrumValues(int code)
    {
        String drumName = " ";
        if (code == 35 || code == 36)
        {
            drumName = "kick drum mole";
        }
        else if (code == 38 || code == 40)
        {
            drumName = "snare drum mole";
        }
        else if (code == 42 || code == 46 || code == 44)
        {
            drumName = "hi hat mole";
        }
        else if (code == 51)
        {
            drumName = "ride cymbal mole";
        }

        return drumName; 
    }

    //validity checks to ensure the name passed in is a real drum 
    //and if so gets its sprite renderer to change 
    void activateSprite(string objectName)
    {
        GameObject drum = GameObject.Find(objectName);

        if (drum != null)
        {
            SpriteRenderer spriteRenderer = drum.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Start coroutine to toggle sprite visibility
                StartCoroutine(ToggleSpriteVisibility(spriteRenderer));
            }
        }
    }

    //function to make a drums mole visible for a specified duration
    private IEnumerator ToggleSpriteVisibility(SpriteRenderer spriteRenderer)
    {
        // Make the sprite visible
        Color visibleColor = spriteRenderer.color;
        visibleColor.a = 1f;
        spriteRenderer.color = visibleColor;

        // Wait for specified time, adjust this according to speed. was 0.35f
        yield return new WaitForSeconds(0.25f);

        // Make the sprite transparent again
        Color transparentColor = spriteRenderer.color;
        transparentColor.a = 0f;
        spriteRenderer.color = transparentColor;
    }
}
