using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SongChooserManager : MonoBehaviour
{
    public UIDocument uiDocument; 
    
    void Start()
    {
        var root = uiDocument.rootVisualElement;    
        root.Q<Button>("song0").clicked += loadSong0;
        root.Q<Button>("song1").clicked += loadSong1;
        root.Q<Button>("song2").clicked += loadSong2;
        root.Q<Button>("song3").clicked += loadSong3;
        root.Q<Button>("song4").clicked += loadSong4;
        root.Q<Button>("song5").clicked += loadSong5;
        root.Q<Button>("song6").clicked += loadSong6;
        root.Q<Button>("song7").clicked += loadSong7;
        root.Q<Button>("song8").clicked += loadSong8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadSong0()
    {
        playDrums.songIndex = 0; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong1()
    {
        playDrums.songIndex = 1; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong2()
    {
        playDrums.songIndex = 2; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong3()
    {
        playDrums.songIndex = 3; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong4()
    {
        playDrums.songIndex = 4; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong5()
    {
        playDrums.songIndex = 5; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong6()
    {
        playDrums.songIndex = 6; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong7()
    {
        playDrums.songIndex = 7; 
        SceneManager.LoadScene("2dScene");
    }

    void loadSong8()
    {
        playDrums.songIndex = 8; 
        SceneManager.LoadScene("2dScene");
    }

}
