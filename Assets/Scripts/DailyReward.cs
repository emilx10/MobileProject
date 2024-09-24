using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public Button rewardButton;
    public List<GameObject> CheckMark;
    public int totalDays = 7;
    public Text coinsText; // Add your UI Text for displaying coins
    public int coinsPerDay = 10; // Reward coins per day

    private DateTime lastRewardTime;
    private int currentDay;
    private bool canClaim;
    private int playerCoins;

    void Start()
    {
        ResetProgress();
        // Correct capitalization of PlayerPrefs keys
        if (PlayerPrefs.HasKey("LastRewardTime"))
        {
            string savedTime = PlayerPrefs.GetString("LastRewardTime");
            lastRewardTime = DateTime.Parse(savedTime);
            Debug.Log("Last reward time: " + lastRewardTime);
        }
        else
        {
            lastRewardTime = DateTime.Now.AddDays(-1);
            Debug.Log("Last reward time: " + lastRewardTime);
        }

        currentDay = PlayerPrefs.GetInt("CurrentRewardDay", 0);

        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        UpdateCoinsText();

        CheckRewardAvailability();
    }

    void CheckRewardAvailability()
    {
        TimeSpan timeSinceLastReward = DateTime.Now - lastRewardTime;

        // Check if 1 day has passed since the last claim
        if (timeSinceLastReward.TotalDays >= 1)
        {
            canClaim = true;
            rewardButton.interactable = true;
            Debug.Log("You can Claim!");
        }
        else
        {
            canClaim = false;
            rewardButton.interactable = false;
        }

        Debug.Log("Last reward time: " + lastRewardTime);
        Debug.Log("Time Since Last Reward: " + timeSinceLastReward);

        // Update checkmarks based on the current day
        UpdateCheckMarks();
    }

    public void ClaimReward()
    {
        if (canClaim)
        {
            lastRewardTime = DateTime.Now;
            PlayerPrefs.SetString("LastRewardTime", lastRewardTime.ToString());
            PlayerPrefs.Save();

            // Add coins for the current day
            playerCoins += coinsPerDay;
            PlayerPrefs.SetInt("PlayerCoins", playerCoins);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Menu Coins", playerCoins);
            PlayerPrefs.Save();
            UpdateCoinsText();

            currentDay++;
            if (currentDay > totalDays)
            {
                currentDay = 1;
            }
            PlayerPrefs.SetInt("CurrentRewardDay", currentDay);
            PlayerPrefs.Save();

            canClaim = false;
            rewardButton.interactable = false;

            // Update check marks
            UpdateCheckMarks();
        }
    }

    private void UpdateCheckMarks()
    {
        // Loop through all checkmarks and update them
        for (int i = 0; i < totalDays; i++)
        {
            if (i < currentDay)
            {
                CheckMark[i].SetActive(true);  // Activate check mark for past days
            }
            else
            {
                CheckMark[i].SetActive(false); // Deactivate check mark for future days
            }
        }
    }

    private void UpdateCoinsText()
    {
        // Update the coin text to reflect the current coin balance
        coinsText.text = "" + playerCoins;
    }

    // Optional: Method to reset progress for testing
    public void ResetProgress()
    {
        // Delete all saved progress in PlayerPrefs
        PlayerPrefs.DeleteKey("LastRewardTime");
        PlayerPrefs.DeleteKey("CurrentRewardDay");
        PlayerPrefs.DeleteKey("PlayerCoins");

        // Reset the local variables
        playerCoins = 0;
        currentDay = 1;
        lastRewardTime = DateTime.Now.AddDays(-1); // So they can claim again if desired

        // Update the coin text
        UpdateCoinsText();

        // Reset the checkmarks' visual states (turn them all off)
        foreach (GameObject checkMark in CheckMark)
        {
            checkMark.SetActive(false);  // Disable all checkmarks
        }

        // Disable the claim button initially
        rewardButton.interactable = false;

        // Call CheckRewardAvailability to refresh the state of the reward system
        CheckRewardAvailability();
    }


}
