using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject leftHitZone;
    public GameObject downHitZone;
    public GameObject upHitZone;
    public GameObject rightHitZone;

    public GameObject leftArrow;
    public GameObject downArrow;
    public GameObject upArrow;
    public GameObject rightArrow;

    private static float ARROW_SPAWN_INTERVAL_SECONDS = 1f;

    private float leftArrowSpawnX;
    private float downArrowSpawnX;
    private float upArrowSpawnX;
    private float rightArrowSpawnX;

    private float spawnY;

    private float timerSeconds = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnY = this.transform.position.y;
        this.leftArrowSpawnX = this.leftHitZone.transform.position.x;
        this.downArrowSpawnX = this.downHitZone.transform.position.x;
        this.upArrowSpawnX = this.upHitZone.transform.position.x;
        this.rightArrowSpawnX = this.rightHitZone.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        this.timerSeconds += Time.deltaTime;
        if (this.timerSeconds >= ArrowSpawner.ARROW_SPAWN_INTERVAL_SECONDS)
        {
            this.SpawnArrow(this.GetRandomArrowDirection());
            this.timerSeconds -= ArrowSpawner.ARROW_SPAWN_INTERVAL_SECONDS;
        }
    }

    private void SpawnArrow(Arrow.Direction direction)
    {
        switch (direction)
        {
            case Arrow.Direction.Left:
                this.SpawnLeftArrow();
                break;
            case Arrow.Direction.Down:
                this.SpawnDownArrow();
                break;
            case Arrow.Direction.Up:
                this.SpawnUpArrow();
                break;
            case Arrow.Direction.Right:
                this.SpawnRightArrow();
                break;
            default:
                this.SpawnLeftArrow();
                break;
        }
    }

    private void SpawnLeftArrow()
    {
        GameObject leftArrow = Instantiate(this.leftArrow, new Vector3(this.leftArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.leftHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(leftArrow);
    }

    private void SpawnDownArrow()
    {
        GameObject downArrow = Instantiate(this.downArrow, new Vector3(this.downArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.downHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(downArrow);
    }

    private void SpawnUpArrow()
    {
        GameObject upArrow = Instantiate(this.upArrow, new Vector3(this.upArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.upHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(upArrow);
    }

    private void SpawnRightArrow()
    {
        GameObject rightArrow = Instantiate(this.rightArrow, new Vector3(this.rightArrowSpawnX, this.spawnY, 0), Quaternion.identity);
        HitZone hitZone = this.rightHitZone.GetComponent<HitZone>();
        hitZone.AddArrow(rightArrow);
    }

    private Arrow.Direction GetRandomArrowDirection()
    {
        int range = Random.Range(0, 4);
        switch (range)
        {
            case 0:
                return Arrow.Direction.Left;
            case 1:
                return Arrow.Direction.Down;
            case 2:
                return Arrow.Direction.Up;
            case 3:
                return Arrow.Direction.Right;
            default:
                return Arrow.Direction.Left;
        }
    }
}
