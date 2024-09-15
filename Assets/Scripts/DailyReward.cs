using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public Button rewardButton;
    public Text rewardText;
    public int totalDays = 7;

    private DateTime lastRewardTime;
    private int currentDay;
    private bool canClaim;

    void Start()
    {
        if(PlayerPrefs.HasKey("LastRewardTime"))
        {
            string savedTime = PlayerPrefs.GetString("LastRewardTime");
            lastRewardTime = DateTime.Parse(savedTime);
               
        }
        else
        {
            lastRewardTime = DateTime.Now.AddDays(-1);
        }
        currentDay = PlayerPrefs.GetInt("Current reward day", 1);

        CheckRewardAvailability();
    }
    void CheckRewardAvailability()
    {
        TimeSpan timeSinceLastReward = DateTime.Now - lastRewardTime;

        if(timeSinceLastReward.TotalDays >= 1)
        {
            canClaim = true;
            rewardText.text = "Claim";
            rewardButton.interactable = true;
        }
        else
        {
            canClaim= false;
            rewardText.text = "Cant claim yet";
            rewardButton.interactable = false;
        }
    }

    public void ClaimReward()
    {
        if(canClaim) //customize this part 
        {
            Debug.Log("Daily reward claimed");
            lastRewardTime = DateTime.Now;
            PlayerPrefs.SetString("Last reward time", lastRewardTime.ToString());
            PlayerPrefs.Save();

            currentDay++;
            if(currentDay > totalDays)
            {
                currentDay = 1;
            }
            PlayerPrefs.SetInt("CurrentRewardDay", currentDay);
            PlayerPrefs.Save();

            CheckRewardAvailability();
        }
    }

    
}
