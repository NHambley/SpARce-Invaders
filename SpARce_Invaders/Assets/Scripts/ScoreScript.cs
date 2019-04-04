using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private int score;
    private int highScore;
    private bool scoreChanged;
    private GameObject HUD;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreChanged = false;
        highScore = GetHighScore();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        text = HUD.GetComponent<Text>();
    }

    void Update()
    {
        if(scoreChanged)
        {
            text.text = "Score: " + score;
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
