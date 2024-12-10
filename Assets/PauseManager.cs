using UnityEngine;
using MidiPlayerTK; // Ensure you include this namespace for MidiPlayerTK methods

public class PauseManager : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer; // Reference to the MidiFilePlayer
     public AudioSource radioactiveAudioSource; // Reference to the Radioactive AudioSource
     public GameObject endGameButton; // Reference to End Game Button
     public GameObject PAUSED; // Reference to PAUSED Button
    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f;

        // Show the End Game Button
            if (PAUSED != null)
             {
                PAUSED.SetActive(true);
             }
              // Show the PAUSED Text
            if (endGameButton != null)
            {
             endGameButton.SetActive(true);
            }
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

              // Hide the End Game Button
             if (endGameButton != null)
            endGameButton.SetActive(false);

                // Hide the PAUSED Text
            if (PAUSED != null)
             PAUSED.SetActive(false);

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


public void EndGame()
    {

    Debug.Log("Game Ended");
    // Add logic to quit or restart the game
       Application.Quit();

       #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



