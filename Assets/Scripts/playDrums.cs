using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using MidiPlayerTK;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Experimental.AI;

public class playDrums : MonoBehaviour
{
    public static int songIndex { get; set; } 
    public MidiFilePlayer midiFilePlayer; 
    private bool isPlaying = false; // track play status locally to sync with SongPlayer.cs
    private int targetChannel = 9;
    public AudioClip kickSound;
    public AudioClip snareSound;
    public AudioClip hiHatSound;
    public AudioClip rideCymbalSound;
    private AudioSource audioSource;
    public AudioSource songAudioSource; //unused?
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
        
        midiFilePlayer.OnEventNotesMidi.AddListener(OnMidiEvent);
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartDrums() {
        if (!isPlaying) {
            isPlaying = true; // toggle
            midiFilePlayer.MPTK_Play(alreadyLoaded: true);
            Debug.Log("Drums started.");
        }
    }

    public void PauseDrums() {
        if (isPlaying) {
            isPlaying = false; // toggle
            midiFilePlayer.MPTK_Pause();
            Debug.Log("Drums paused.");
        }
    }

    public void ResumeDrums() {
        if (!isPlaying)
        {
            midiFilePlayer.MPTK_UnPause();
            isPlaying = true;
            Debug.Log("Drums resumed.");
        }
    }

    public void StopDrums() {
        if (isPlaying)
        {
            midiFilePlayer.MPTK_Stop();
            isPlaying = false;
            Debug.Log("Drums stopped.");
        }
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
                    //long noteDuration = midiEvent.Tick / midiFilePlayer.MPTK_DeltaTicksPerQuarterNote;
                    Debug.Log($"Note duration: {midiEvent.Duration / 1000.0f}");
                    activateSprite(formatDrumValues(midiEvent.Value), midiEvent.Duration / 100.0f);
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

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         StartCoroutine(sleep());
    //     }
    //     else if(Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         midiFilePlayer.MPTK_Stop();
    //     }
    //     if(Input.GetKeyDown(KeyCode.H))
    //     {
    //         PlaySound(hiHatSound);
    //         TriggerDrum(42);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.J)) // Snare Drum
    //     {
           
    //         PlaySound(snareSound);
    //         TriggerDrum(38);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.K)) // Kick Drum
    //     {
    //         PlaySound(kickSound);
    //         TriggerDrum(35);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.L)) // Ride Cymbal
    //     {
    //         PlaySound(rideCymbalSound);
    //         TriggerDrum(51);
    //     }
    // }

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
    
    

    // void TriggerDrum(int drumValue)
    // {
    //     string drumName = formatDrumValues(drumValue);
        
    //     if(drumName == " ")
    //     {
    //         Debug.LogWarning("Invalid drum type!");
    //         return; 
    //     }

    //     Debug.Log($"Triggered {drumName}");
    //     activateSprite(drumName);
    // }

    void activateSprite(string objectName, float duration)
    {
        GameObject drum = GameObject.Find(objectName);

        if (drum != null)
        {
            SpriteRenderer spriteRenderer = drum.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Start coroutine to toggle sprite visibility
                StartCoroutine(ToggleSpriteVisibility(spriteRenderer, duration));
            }
        }
    }

    private IEnumerator ToggleSpriteVisibility(SpriteRenderer spriteRenderer, float duration)
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