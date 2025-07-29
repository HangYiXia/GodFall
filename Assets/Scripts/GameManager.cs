using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    
    private int heightWavesSurvived;
    private const string heightWavesSurvivedKey = "HighScore";

    private void Start()
    {
        GameIsOver = false;
        heightWavesSurvived = PlayerPrefs.GetInt(heightWavesSurvivedKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver) return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        if (PlayerStats.Rounds > heightWavesSurvived)
        {
            heightWavesSurvived = PlayerStats.Rounds;
            PlayerPrefs.SetInt(heightWavesSurvivedKey, heightWavesSurvived);
            PlayerPrefs.Save();
        }
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
