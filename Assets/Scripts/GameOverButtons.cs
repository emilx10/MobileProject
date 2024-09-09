using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{

    public void HighScore()
    {
        SceneManager.LoadSceneAsync("HighScoreScreen");
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void BackToGameOverMenu()
    {
        SceneManager.LoadSceneAsync("GameOverScene");
    }
}


