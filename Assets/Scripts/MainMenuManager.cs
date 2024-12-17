using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Created by Galen Ferrara - 8/12/2024

public class MainMenuManager : MonoBehaviour
{
    public UIDocument uiDocument;

    void Start()
    {
        //gets the visual element that holds the buttons
        var root = uiDocument.rootVisualElement;

        //connects the button with its corresponding function to perform the action 
        root.Q<Button>("PlayButton").clicked += LoadGame;
        root.Q<Button>("TutorialButton").clicked += LoadTutorial;
        root.Q<Button>("QuitButton").clicked += QuitGame;

    }

    //lets the user go back by pressing the escape key
    void Update()
    {
        // Listen for Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnHome();
        }
    }

    //loads the tutorial scene
    void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    //loads the song chooser scene 
    void LoadGame()
    {
        //SceneManager.LoadScene("2dScene");
        SceneManager.LoadScene("SongChooser");
    }

    //loads the menu scene 
    void ReturnHome() {
        SceneManager.LoadScene("menu"); // Return to the menu
    }

    //closes the program 
    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
