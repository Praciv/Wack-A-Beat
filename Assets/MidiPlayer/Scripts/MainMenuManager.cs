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

    void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    void LoadGame()
    {
        SceneManager.LoadScene("2dScene");
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
