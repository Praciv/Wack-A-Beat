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

public class playDrums : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer; 
    private bool isPlaying = false;
    public Sprite mole; 
    private int targetChannel = 9;

    public AudioClip kickSound;
    public AudioClip snareSound;
    public AudioClip hiHatSound;
    public AudioClip rideCymbalSound;
    private AudioSource audioSource;
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

        audioSource = gameObject.AddComponent<AudioSource>();

    
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
                    changeSprite(formatDrumValues(midiEvent.Value));
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
            PlaySound(hiHatSound);
            TriggerDrum(42);
        }
        else if (Input.GetKeyDown(KeyCode.J)) // Snare Drum
        {
           
            PlaySound(snareSound);
            TriggerDrum(38);
        }
        else if (Input.GetKeyDown(KeyCode.K)) // Kick Drum
        {
            PlaySound(kickSound);
            TriggerDrum(35);
        }
        else if (Input.GetKeyDown(KeyCode.L)) // Ride Cymbal
        {
            PlaySound(rideCymbalSound);
            TriggerDrum(51);
        }
}

String formatDrumValues(int code)
{
    String drumName = " ";
    if (code == 35 || code == 36)
    {
        drumName = "kick drum";
    }
    else if (code == 38 || code == 40)
    {
        drumName = "snare drum";
    }
    else if (code == 42)
    {
        drumName = "hi hat";
    }
    else if (code == 51)
    {
        drumName = "ride cymbal";
    }

    return drumName; 
}


void PlaySound(AudioClip clip)
{
    if (clip != null)
    {
        Debug.Log($"Playing sound: {clip.name}");
        audioSource.PlayOneShot(clip);
    }
    else
    {
        //Debug.LogWarning($"No sound assigned for this drum! Called from: {new System.Diagnostics.StackTrace()}");
    }
    
}
    
    

void TriggerDrum(int drumValue)
{
    string drumName = formatDrumValues(drumValue);
    
    if(drumName == " ")
    {
        Debug.LogWarning("Invalid drum type!");
        return; 
    }

    Debug.Log($"Triggered {drumName}");
    changeSprite(drumName);
}

void changeSprite(string objectName)
{
    GameObject drum = GameObject.Find(objectName);

    if (drum != null)
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);

        Color colour = new Color(r, g, b);
        drum.GetComponent<SpriteRenderer>().color = colour;

    }
    else
    {
        Debug.LogError($"Drum object '{objectName}' not found!");
    }

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

    private IEnumerator ToggleSpriteVisibility(SpriteRenderer spriteRenderer)
    {
        // Make the sprite visible
        Color visibleColor = spriteRenderer.color;
        visibleColor.a = 1f;
        spriteRenderer.color = visibleColor;

        // Wait for specified time, adjust this according to speed
        yield return new WaitForSeconds(0.35f);

        // Make the sprite transparent again
        Color transparentColor = spriteRenderer.color;
        transparentColor.a = 0f;
        spriteRenderer.color = transparentColor;
    }
}
