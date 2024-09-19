using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCoinManager : MonoBehaviour
{
    public Text coinsText;

    void Start()
    {
        coinsText.text = ""+PlayerPrefs.GetInt("PlayerCoins"); 
    }

}
