using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Created by Galen Ferrara - 8/12/2024

public class MainMenuManager : MonoBehaviour
{
    public UIDocument uiDocument;

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        root.Q<Button>("PlayButton").clicked += LoadGame;
        root.Q<Button>("TutorialButton").clicked += LoadTutorial;
        root.Q<Button>("QuitButton").clicked += QuitGame;

    }

    void Update()
    {
        // Listen for Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnHome();
        }
    }

    void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    void LoadGame()
    {
        //SceneManager.LoadScene("2dScene");
        SceneManager.LoadScene("songChooser");
    }

    void ReturnHome() {
        SceneManager.LoadScene("menu"); // Return to the menu
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
