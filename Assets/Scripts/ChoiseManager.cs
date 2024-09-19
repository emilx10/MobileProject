using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseManager : MonoBehaviour
{
    private string controlScheme;
    public List<GameObject> Arrows;

    void Start()
    {
        controlScheme = PlayerPrefs.GetString("ControlScheme", "Arrows");

        if (controlScheme == "Swipe")
        {
            EnableSwipeControls();
        }
        else if (controlScheme == "Arrows")
        {
            EnableArrowControls();
        }
    }

    void EnableSwipeControls()
    {
        for (int i = 0; i < Arrows.Count; i++)
        {
            Arrows[i].SetActive(false);
        }
    }

    void EnableArrowControls()
    {
        Debug.Log("Arrow controls enabled");
    }
}

