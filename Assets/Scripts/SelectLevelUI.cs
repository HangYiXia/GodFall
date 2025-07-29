using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelUI : MonoBehaviour
{
    public Button[] levelButtons;

    public SceneFader sceneFader;
    public string menuSceneName = "Menu";

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = levelReached; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
