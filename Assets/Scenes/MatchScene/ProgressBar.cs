using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public static int PLAYER_ONE_WIN_SCORE = 10;
    public static int PLAYER_TWO_WIN_SCORE = -10;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return this.score;
    }

    public void IncrementScore(int amount)
    {
        this.score += amount;
        if (this.score > PLAYER_ONE_WIN_SCORE)
        {
            this.score = PLAYER_ONE_WIN_SCORE;
        }
    }

    public void DecrementScore(int amount)
    {
        this.score -= amount;
        if (this.score < PLAYER_TWO_WIN_SCORE)
        {
            this.score = PLAYER_TWO_WIN_SCORE;
        }
    }

    public bool IsGameWonByScore()
    {
        return score >= PLAYER_ONE_WIN_SCORE || score <= PLAYER_TWO_WIN_SCORE;
    }
}
