using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    public AudioSource songAudioSource; // Reference to the AudioSource playing the song
    public float delay = 0.1f; // Delay in seconds before playing the song
    private bool isPaused = true;

    void Start()
    {
        // Check if the AudioSource is assigned
        if (songAudioSource == null)
        {
            Debug.LogError("Song AudioSource is not assigned!");
            return;
        }

        Invoke(nameof(PlaySong), delay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopSong();
        }
    }

    private void PlaySong()
    {
        if (songAudioSource !=null && !songAudioSource.isPlaying)
        {
            songAudioSource.Play();
            Debug.Log($"Song started.");
        }
    }

    private void TogglePause()
    {
        if (songAudioSource == null)
            return;

        isPaused = !isPaused;

        if (isPaused)
        {
            songAudioSource.Pause();
            Debug.Log("Song Paused");
        }
        else
        {
            songAudioSource.UnPause();
            Debug.Log("Song Resumed");
        }
    }

    private void StopSong()
    {
        if (songAudioSource == null)
            return;

        songAudioSource.Stop();
        isPaused = false; // Reset the pause state
        Debug.Log("Song Stopped");
    }
}