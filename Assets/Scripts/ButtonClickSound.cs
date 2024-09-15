using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
   
    void Start()
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
}
