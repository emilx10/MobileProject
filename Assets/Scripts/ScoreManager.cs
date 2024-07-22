using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    private float score = 0f;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
            UpdateScoreText();
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

    IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }
}
