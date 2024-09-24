using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private int resetscore = 0;
    public void StartGame()
    {
        PlayerPrefs.SetInt("StartingScore", resetscore);
        SceneManager.LoadSceneAsync("Game");
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("StartingScore", resetscore);
        Application.Quit();
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("StartingScore", resetscore);
        SceneManager.LoadSceneAsync("Main menu");
    }

    public void CheckPointScene()
    {
        PlayerPrefs.SetInt("StartingScore", resetscore);
        SceneManager.LoadSceneAsync("CheckPointSelect");
    }
   
    public void BackToEndMenu()
    {
        SceneManager.LoadSceneAsync("End Game");
    }
}
