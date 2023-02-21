using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSyncDebugger : MonoBehaviour
{
    public KeyCode spawnMarkerKey;
    public BeatMarker beatMarker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnMarkerKey))
        {
            this.SpawnBeatMarker();
        }
    }

    private void SpawnBeatMarker()
    {
        BeatMarker beatMarker = Instantiate(this.beatMarker, this.transform.position, Quaternion.identity);
        SpriteRenderer beatMarkerSprite = beatMarker.GetComponent<SpriteRenderer>();
        Color color = beatMarkerSprite.color;
        color.b -= 0.5f;
        beatMarkerSprite.color = color;
    }
}
