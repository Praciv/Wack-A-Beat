using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using MidiPlayerTK;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Experimental.AI;

public enum drumkit 
{
    NODRUM = 0,
    KICKDRUM = 35,
    SNAREDRUM = 38,
    HIHAT = 42,
    RIDECYMBAL = 51
}

public class playDrums : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer; 
    public drumkit drum;
    public Timer timer; 
    private bool isPlaying = false;
    public Sprite mole; 
    private int targetChannel = 9;

    void Start()
    {
        midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();

        if(midiFilePlayer == null)
        {
            Debug.LogError("MidiFilePlayer not found in the scene");
            return;
        }

        midiFilePlayer.MPTK_MidiIndex = 27;

        MidiLoad midiLoaded = midiFilePlayer.MPTK_Load();
        
        midiFilePlayer.OnEventNotesMidi.AddListener(OnMidiEvent);

        midiFilePlayer.MPTK_Play(alreadyLoaded: true);  
    }

     void OnMidiEvent(List<MPTKEvent> midiEvents)
    {
        foreach (var midiEvent in midiEvents)
        {
            // Check if the event is on the target channel
            if (midiEvent.Channel == targetChannel)
            {
                if(midiEvent.Command == MPTKCommand.NoteOn && midiEvent.Velocity > 0)
                {
                    //MIDI values: https://www.music.mcgill.ca/~ich/classes/mumt306/StandardMIDIfileformat.html#BMA1_3
                    Debug.Log($"Channel: {midiEvent.Channel}, Note: {midiEvent.Value}, Velocity: {midiEvent.Velocity}, Time: {midiEvent.RealTime}");
                    if (midiEvent.Value == 35 || midiEvent.Value == 36)
                    {
                        changeSprite("kick drum");
                    }
                    else if (midiEvent.Value == 38 || midiEvent.Value == 40)
                    {
                        changeSprite("snare drum");
                    }
                    else if (midiEvent.Value == 42)
                    {
                        //Debug.Log("hi-hat");
                        changeSprite("hi hat");
                    }
                    else if (midiEvent.Value == 51)
                    {
                        changeSprite("ride cymbal");
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (midiFilePlayer != null)
            midiFilePlayer.OnEventNotesMidi.RemoveListener(OnMidiEvent);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = true; 
            //audioSource = audio.GetComponent<AudioSource>();
            //audioSource.Play();
            changeSprite("hi-hat");
        }
    }

    void changeSprite(String objectName)
    {
        GameObject drum = GameObject.Find(objectName);
        
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        
        Color colour = new Color(r, g, b);
        drum.GetComponent<SpriteRenderer>().color = colour;
    }
}
