using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckPointCoinsbutton : MonoBehaviour
{
    public Text playerCoinsAmount;
    public Text CoinsNeded;
    private int playerCoins;
    private int CoinsRequired;
    private void Start()
    {
        playerCoins = int.Parse(playerCoinsAmount.text);
        CoinsRequired = int.Parse(CoinsNeded.text);
    }

    public void AmountOfCoinsCheck()
    {
        if (playerCoins - CoinsRequired >= 0)
        {
            playerCoins -= CoinsRequired;
            playerCoinsAmount.text = playerCoins.ToString();

            PlayerPrefs.SetInt("StartingScore", CoinsRequired);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Game");

        }
    }
}
