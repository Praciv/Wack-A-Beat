using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    public AudioSource songAudioSource; // Reference to the AudioSource playing the song
    public float delay = 0.1f; // Delay in seconds before playing the song

    void Start()
    {
        // Check if the AudioSource is assigned
        if (songAudioSource == null)
        {
            Debug.LogError("Song AudioSource is not assigned!");
            return;
        }

        // Play the song with a delay
        songAudioSource.PlayDelayed(delay);
        Debug.Log($"Song will play with a delay of {delay} seconds");
    }
}