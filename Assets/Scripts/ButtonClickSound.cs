using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public void Start()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Add the listener for when the button is clicked
        button.onClick.AddListener(PlayClickSound);


    }

    void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void SelectSwipeControls()
    {
        PlayerPrefs.SetString("ControlScheme", "Swipe");
        PlayerPrefs.Save();
    }

    public void SelectArrowControls()
    {
        PlayerPrefs.SetString("ControlScheme", "Arrows");
        PlayerPrefs.Save();
    }
}
