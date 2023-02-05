using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{

    public int difficulty; // 0 to 10
    private static int MAX_DIFF = 1;

    private static int PERFECT_RNG = 600;
    private static int GOOD_HIT_RNG = 700;

    public float nextHitTiming = 0.0f;

    private int baseProbability = 500;

    public PlayerAI(int difficulty){
        this.difficulty = difficulty;
    }

    public void generateNextHitTiming(int combo){
        //Debug.Log("Incoming Combo: "+ combo);
        //Debug.Log("diff modifier: "+ Mathf.Floor(combo / 2) * (2 * difficulty + 1));
        int currentProbability = baseProbability + (int)(Mathf.Floor(combo / 2) * (2 * difficulty + 1));
        int hitRNG = Random.Range(0, currentProbability);
        // Perfect hit
        if (hitRNG < PERFECT_RNG){
            // get timing window that will result in perfect hit
            nextHitTiming = 0.0f;
        }
        // Good Hit
        else if (hitRNG < GOOD_HIT_RNG){
            // decide whether it's early or late
            nextHitTiming = 0.3f;
        }
        // Miss
        else{
            // Decide whether it's early or late
            nextHitTiming = - 1f;
            //Debug.Log("MISS");
        }
    }
}
