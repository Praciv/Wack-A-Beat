using UnityEngine;
using MidiPlayerTK; // Ensure you include this namespace for MidiPlayerTK methods

public class PauseManager : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer; // Reference to the MidiFilePlayer
     public AudioSource radioactiveAudioSource; // Reference to the Radioactive AudioSource
    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f;

             // Pause the Radioactive AudioSource
            if (radioactiveAudioSource != null && radioactiveAudioSource.isPlaying)
            {
                radioactiveAudioSource.Pause();
                Debug.Log("Radioactive AudioSource Paused");
            }

            // Pause the MidiFilePlayer
            if (midiFilePlayer != null)
            {
                midiFilePlayer.MPTK_Pause();
                Debug.Log("MIDI Paused");
            }

            Debug.Log("Game Paused");
        }
        else
        {
            // Resume the game
            Time.timeScale = 1f;

                  // Resume the Radioactive AudioSource
            if (radioactiveAudioSource != null)
            {
                radioactiveAudioSource.UnPause();
                Debug.Log("Radioactive AudioSource Resumed");
            }

            // Resume the MidiFilePlayer
            if (midiFilePlayer != null)
            {
                midiFilePlayer.MPTK_UnPause();
                Debug.Log("MIDI Resumed");
            }

            Debug.Log("Game Resumed");
        }
    }
}