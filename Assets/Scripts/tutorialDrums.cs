using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Enables manual control of drums using the numers 1-4

public class TutorialDrums : MonoBehaviour
{
    public AudioClip kickSound;
    public AudioClip snareSound;
    public AudioClip hiHatSound;
    public AudioClip rideCymbalSound;

    private AudioSource audioSource;

    //assigns the audioSource variable 
    void Start()
    {
        // Attach AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    //checks if certain keys are pressed and triggers the relevant drum 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Key '1'
        {
            PlaySound(kickSound);
            TriggerDrum(35); // Kick Drum
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key '2'
        {
            PlaySound(snareSound);
            TriggerDrum(38); // Snare Drum
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Key '3'
        {
            PlaySound(hiHatSound);
            TriggerDrum(42); // Hi-Hat
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // Key '4'
        {
            PlaySound(rideCymbalSound);
            TriggerDrum(51); // Ride Cymbal
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }

    }

    //plays the specified AudioClip once 
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            Debug.Log($"Playing sound: {clip.name}");
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No sound assigned");
        }
    }

    // Method handles showing the mole for the drum being pressed
    void TriggerDrum(int drumValue)
    {
        string drumName = FormatDrumValues(drumValue);

        if (string.IsNullOrEmpty(drumName))
        {
            Debug.LogWarning("Invalid drum type!");
            return;
        }

        Debug.Log($"Triggered {drumName}");
        ActivateSprite(drumName);
    }

    //MIDI values: https://www.music.mcgill.ca/~ich/classes/mumt306/StandardMIDIfileformat.html#BMA1_3
    // Handles MIDI values
    string FormatDrumValues(int code)
    {
        switch (code)
        {
            case 35:
            case 36:
                return "kick drum mole";
            case 38:
            case 40:
                return "snare drum mole";
            case 42:
                return "hi hat mole";
            case 51:
                return "ride cymbal mole";
            default:
                return string.Empty;
        }
    }

    //validity checks to ensure the name passed in is a real drum 
    //and if so gets its sprite renderer to change 
    void ActivateSprite(string objectName)
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

        // Wait for specified time
        // Could change this to show for as long as the tutorial-person wants
        yield return new WaitForSeconds(0.25f);

        // Make the sprite transparent again
        Color transparentColor = spriteRenderer.color;
        transparentColor.a = 0f;
        spriteRenderer.color = transparentColor;
    }
}
