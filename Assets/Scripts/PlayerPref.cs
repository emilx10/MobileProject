using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPref : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;

    public void SaveData()
    {
        if (nameInputField != null && ageInputField != null)
        {
            PlayerPrefs.SetString("PlayerName", nameInputField.text);
            PlayerPrefs.SetString("PlayerAge", ageInputField.text);
        }
    }

    public  void LoadData()
    {
        nameInputField.text = PlayerPrefs.GetString("PlayerName");
        ageInputField.text = PlayerPrefs.GetString("PlayerAge");
    }
}
