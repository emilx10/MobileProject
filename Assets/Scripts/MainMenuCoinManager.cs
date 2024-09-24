using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCoinManager : MonoBehaviour
{
    public Text coinsText;
    private int coinsMenuAmount = 0;
    void Start()
    {
        PlayerPrefs.SetInt("Menu Coins", coinsMenuAmount);
        PlayerPrefs.Save();
        coinsText.text = ""+PlayerPrefs.GetInt("Menu Coins");
    }

    private void Update()
    {
        coinsText.text = "" + PlayerPrefs.GetInt("Menu Coins");
    }

}
