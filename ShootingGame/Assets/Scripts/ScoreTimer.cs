using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI timescoreText;
    public int realScore = 0;
    public float timeScore = 0;

    public static ScoreTimer scoreTimer;

    public static float highScore = 0;

    void Start()
    {
        scoreTimer = this;
    }

    void Update()
    {
        // VERY UNOPTIMIZED AND TERRIBLE CODE. I KNOW.
        if (!GameStateManager.alive)
        {
            if(realScore <= 0)
            {
                scoreText.gameObject.SetActive(false);
            }
            else
            {
                scoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            timescoreText.gameObject.SetActive(false);
            highscoreText.gameObject.SetActive(false);
        }

        if (!GameStateManager.alive) return;

        timeScore += Time.deltaTime;
    }

    public void Reset()
    {
        if (highScore > 0)
        {
            highscoreText.gameObject.SetActive(true);
            highscoreText.SetText("High Score: " + highScore);
        }

        if (timeScore > 0)
        {
            timescoreText.gameObject.SetActive(true);
            timescoreText.SetText(GetFormattedTime(timeScore));
        }

        realScore = 0;
        timeScore = 0;

        ApplyScore(realScore);
    }

    string GetFormattedTime(float _time)
    {
        string score = string.Format("Time: {0}s", _time.ToString("0.0"));

        return score;
    }

    void ApplyScore(int score)
    {
        scoreText.text = score.ToString();

        if(score > highScore) highScore = score;
    }

    public void ResetScore()
    {
        realScore = 0;

        ApplyScore(realScore);
    }

    public void IncrementScore()
    {
        realScore++;

        ApplyScore(realScore);

        if(realScore > highScore) highScore = realScore;
    }
}
