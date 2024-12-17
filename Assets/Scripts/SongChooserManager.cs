using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SongChooserManager : MonoBehaviour
{
    public UIDocument uiDocument; 
    
    void Start()
    {
        //gets the visual element that holds the buttons
        var root = uiDocument.rootVisualElement;   

        //connects the button with its corresponding function to perform the action 
        root.Q<Button>("song0").clicked += loadSong0;
        root.Q<Button>("song1").clicked += loadSong1;
        root.Q<Button>("song2").clicked += loadSong2;
        root.Q<Button>("song3").clicked += loadSong3;
        root.Q<Button>("song4").clicked += loadSong4;
        root.Q<Button>("song5").clicked += loadSong5;
        root.Q<Button>("song6").clicked += loadSong6;
        root.Q<Button>("song7").clicked += loadSong7;
        root.Q<Button>("song8").clicked += loadSong8;

        root.Q<Button>("speed1").clicked += slowSpeed;
        root.Q<Button>("speed2").clicked += normalSpeed;
    }

    // Update is called once per frame
    //lets the user go back by pressing the escape key
    void Update()
    {
        // Listen for Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnHome();
        }
    }

    //loads the menu scene 
    void ReturnHome() {
        SceneManager.LoadScene("menu"); // Return to the menu
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong0()
    {
        playDrums.songIndex = 0; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong1()
    {
        playDrums.songIndex = 1; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong2()
    {
        playDrums.songIndex = 2; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong3()
    {
        playDrums.songIndex = 3; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong4()
    {
        playDrums.songIndex = 4; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong5()
    {
        playDrums.songIndex = 5; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong6()
    {
        playDrums.songIndex = 6; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong7()
    {
        playDrums.songIndex = 7; 
        SceneManager.LoadScene("2dScene");
    }

    //loads the scene after specifying the songIndex variable 
    void loadSong8()
    {
        playDrums.songIndex = 8; 
        SceneManager.LoadScene("2dScene");
    }

    //sets the speed variable  
    void slowSpeed()
    {
        playDrums.speed = 0.75f; 
    }

    //sets the speed variable
    void normalSpeed()
    {
        playDrums.speed = 1f; 
    }
}
