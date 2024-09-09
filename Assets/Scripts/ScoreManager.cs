using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    public GameObject player; // Reference to the player character
    public float speedIncreaseAmount = 1.5f; // Amount to increase speed
    private float score = 0f;
    private bool isGameOver = false;
    private float currentSpeed = 5f; // Assuming you have a base speed for the player or obstacles
    private int lastCheckpointScore = 0;

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
            UpdateScoreText();
            CheckForCheckpoint();
            IncreaseSpeed();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        SaveScore();
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverCoroutine());
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    void SaveScore()
    {
        int currentScore = Mathf.FloorToInt(score);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        PlayerPrefs.SetInt("LastScore", currentScore);
        PlayerPrefs.Save();
    }

    void CheckForCheckpoint()
    {
        // If the score is a multiple of 50 and we haven't saved the checkpoint yet
        if (Mathf.FloorToInt(score) % 20 == 0 && Mathf.FloorToInt(score) != lastCheckpointScore)
        {
            SaveCheckpoint();
            lastCheckpointScore = Mathf.FloorToInt(score); // Store the last checkpoint score
        }
    }

    void SaveCheckpoint()
    {
        // Save player's position
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

        // Save the current score
        PlayerPrefs.SetInt("CheckpointScore", Mathf.FloorToInt(score));

        // Optionally, you can save other game states like the current obstacles, etc.
        PlayerPrefs.Save();

        Debug.Log("Checkpoint Saved at score: " + Mathf.FloorToInt(score));
    }

    void IncreaseSpeed()
    {
        // If the score is a multiple of 50, increase speed
        if (Mathf.FloorToInt(score) % 20 == 0 && Mathf.FloorToInt(score) != lastCheckpointScore)
        {
            currentSpeed += speedIncreaseAmount; // Adjust speed of your object
            // Here you need to apply this speed increase to the player or the moving obstacles
            Debug.Log("Speed increased to: " + currentSpeed);
        }
    }

    IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }
}
