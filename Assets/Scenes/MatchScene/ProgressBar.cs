using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public int score = 0;

    public static int PLAYER_ONE_WIN_SCORE = 10;
    public static int PLAYER_TWO_WIN_SCORE = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsGameWonByScore()
    {
        return score >= PLAYER_ONE_WIN_SCORE || score <= PLAYER_TWO_WIN_SCORE;
    }
}
