using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class CheckpointManager : MonoBehaviour
{
    public int checkpointID;
    async void Start()
    {
        try
        {
            
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            Debug.Log("Analytics initialized successfully");
            AnalyticsService.Instance.StartDataCollection();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to initialize analytics: {ex.Message}");
        }
    }
    void Update()
    {
        PassCheckpoint();
    }

    void PassCheckpoint()
    {
        Debug.Log($"Checkpoint {checkpointID} passed at position {transform.position}");

        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                CustomEvent checkpoint_passed = new CustomEvent("checkpoint_passed");
                AnalyticsService.Instance.RecordEvent(checkpoint_passed);
                AnalyticsService.Instance.Flush();
                Debug.Log("checkpoint passed");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }
    }
}
