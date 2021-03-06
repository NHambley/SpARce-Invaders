﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    ScoreTimerScript scoreS;
    int score;

    int ones, tens, hundreds;

    public GameObject[] numbers;

    public GameObject[] numPos;
    // Start is called before the first frame update
    void Start()
    {
        scoreS = GetComponent<ScoreTimerScript>();
        score = scoreS.GetSessionScore();
        ones = 0;
        tens = 0;
        hundreds = 0;

        DisplayScore();
    }

    void DisplayScore()
    {
        // loop through the score to make sure it works
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    score -= 1;

                    ones = k;
                    if (score == 0)
                        return;
                }

                tens = j;
                if (score == 0)
                    return;
            }
            hundreds = i;
            if (score == 0)
                return;
        }

        // Instantiate the score
        Instantiate(numbers[hundreds], numPos[0].transform.position, Quaternion.identity);
        Instantiate(numbers[tens], numPos[1].transform.position, Quaternion.identity);
        Instantiate(numbers[ones], numPos[2].transform.position, Quaternion.identity);
    }
}
