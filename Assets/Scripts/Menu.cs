using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string sceneToLoad = "MainGameScene";
    public SceneFader sceneFader;

    public Text bestScore;
    private const string heightWavesSurvivedKey = "HighScore";

    void Start()
    {
        bestScore.text = "Best : " + PlayerPrefs.GetInt(heightWavesSurvivedKey, 0).ToString();
    }

    
    public void Paly()
    {
        sceneFader.FadeTo(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
