using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{

    public int difficulty; // 0 to 99

    private static int DEFAULT_PERFECT_RNG = 950;
    private static int DEFAULT_GOOD_HIT_RNG = 800;
    private static int DEFAULT_EARLY_MISS_RNG = 700;
    private static int DEFAULT_LATE_MISS_RNG = 1200;

    public float nextHitTiming = -0.0f;
    private float lastHitTiming = 0.1f;

    public PlayerAI(int difficulty){
        this.difficulty = difficulty;
    }

    public void generateNextHitTiming(int combo){
        int hitRNG = Random.Range(0, 1000);
        //if (hitRNG < ){}

        nextHitTiming = 0.5f;
    }
}
