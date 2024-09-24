using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CheckPointButtonManager : MonoBehaviour
{
    int score;
    public GameObject[] buttons;
    public Text AmountOfCoins;

    private void Start()
    {
        score = PlayerPrefs.GetInt("LastScore", 0);
        Debug.Log("This is the score: "+score);
        AmountOfCoins.text ="" + PlayerPrefs.GetInt("PlayerCoins" , 0);
        if (Mathf.FloorToInt(score) >= 50)
        {
            buttons[0].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 100)
        {
            buttons[1].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 150)
        {
            buttons[2].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 200)
        {
            buttons[3].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 250)
        {
            buttons[4].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 300)
        {
            buttons[5].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 350)
        {
            buttons[6].SetActive(true);
        }
        if (Mathf.FloorToInt(score) >= 400)
        {
            buttons[7].SetActive(true);
        }
    }
}
