using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "Menu";

    public string nextLevel = "Level02";
    public int levelUnlock = 2;
    public void Conttinue()
    {
        PlayerPrefs.SetInt("levelReached", levelUnlock);

        sceneFader.FadeTo(nextLevel);
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
