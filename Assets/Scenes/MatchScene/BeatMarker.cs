using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMarker : MonoBehaviour
{
    public static float HITZONE_Y = 3.6f - 0.25f;
    public static float BEATS_FROM_SPAWN_TO_HITZONE = 8;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        float secondsPerBeat = ArrowSpawner.GetNoteDurationInSeconds(NoteDuration.Whole);
        float secondsToHitZone = BEATS_FROM_SPAWN_TO_HITZONE * secondsPerBeat / 4;
        float distanceToHitZone = Mathf.Abs(this.transform.position.y - HITZONE_Y);
        speed = distanceToHitZone / secondsToHitZone;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
