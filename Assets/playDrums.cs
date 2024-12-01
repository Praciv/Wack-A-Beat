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
    private GameObject audio;
    private AudioSource audioSource; 
    private double[] timeSignatures = {};
    private int[,] beats = { {}, {} };
    public Sprite mole; 
    private int targetChannel = 9;

    void Start()
    {
        //timer.BeginInit();
        audio = GameObject.Find("Audio Manager");
        midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();

        if(midiFilePlayer == null)
        {
            Debug.LogError("MidiFilePlayer not found in the scene");
            return;
        }

        midiFilePlayer.MPTK_MidiIndex = 72;

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
                Debug.Log(midiEvent.Value);
                switch(midiEvent.Value) 
                {
                    case 0:
                    {
                        //Do Nothing
                    } break;
                    case 35:
                    {
                        changeSprite("kick drum");
                    } break;
                    case 38:
                    {
                        changeSprite("snare drum");
                    } break;
                    case 42:
                    {
                        changeSprite("hi-hat");
                    } break;
                    case 51:
                    {
                        changeSprite("ride cymbal");
                    } break;
                    default:
                    break;
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
        GameObject drum = GameObject.Find(name);
        
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        
        Color colour = new Color(r, g, b);
        drum.GetComponent<SpriteRenderer>().color = colour;
    }
}
