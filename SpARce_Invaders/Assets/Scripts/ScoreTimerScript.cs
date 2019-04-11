using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTimerScript : MonoBehaviour
{
    private int score;
    private int highScore;
    private bool scoreChanged;
    private GameObject HUD;
    private GameObject TIME;
    private Text timeText;
    private Text scoreText;
    private float timeLeft;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 90f;
        score = 0;
        scoreChanged = false;
        highScore = GetHighScore();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        scoreText = HUD.GetComponent<Text>();
        TIME = GameObject.FindGameObjectWithTag("TIME");
        timeText = TIME.GetComponent<Text>();
    }

    void Update()
    {
        //timer
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = Mathf.RoundToInt(timeLeft % 60);
        if(minutes < 10)
        {
            text = "0" + minutes.ToString() + ":";
        }
        else
        {
            text += minutes.ToString();
        }
        if(seconds < 10)
        {
            text += "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            text += Mathf.RoundToInt(seconds).ToString();
        }
        timeText.text = text;
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            SaveScore();
            SceneManager.LoadScene("GameOverScene");
        }
        //score
        if (scoreChanged)
        {
            scoreText.text = "Score: " + score;
            scoreChanged = false;
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreChanged = true;
    }

    public void SaveScore()
    {
        if (highScore < score)
        {
            PlayerPrefs.SetInt("HiScore", score);
        }
        PlayerPrefs.SetInt("SessionScore", score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HiScore");
    }
    public int GetSessionScore()
    {
        return score;
    }
}
