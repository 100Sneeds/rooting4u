using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public KeyCode activationKey;

    public enum SuccessLevel
    {
        HasNotReachedHitZone,
        Early,
        Late,
        Good,
        Perfect,
    }

    private static float PRESSED_SPRITE_SCALING_FACTOR = 0.9f;

    private static float PERFECT_HIT_MAX_DISTANCE = 0.2f;

    private static float GOOD_HIT_MAX_DISTANCE = 0.4f;
    private static float EARLY_MISS_MAX_DISTANCE = 0.8f;
    private static float LATE_MISS_MAX_DISTANCE = 0.2f;

    private static float MISSED_ARROW_DESTROY_DELAY = 4.0f;

    private Vector3 initialScale;
    private Vector3 pressedScale;

    private Queue<GameObject> arrowQueue = new Queue<GameObject>();
    private GameObject currentArrow;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = this.transform.localScale;
        pressedScale = new Vector3(
            initialScale.x * HitZone.PRESSED_SPRITE_SCALING_FACTOR,
            initialScale.y * HitZone.PRESSED_SPRITE_SCALING_FACTOR,
            initialScale.z * HitZone.PRESSED_SPRITE_SCALING_FACTOR
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentArrow != null && this.IsArrowPastHitZone(this.currentArrow))
        {
            this.ProcessArrowMiss(this.currentArrow);
        }
        if (Input.GetKeyDown(activationKey))
        {
            if (this.currentArrow != null)
            {
                this.ProcessArrowActivation(this.currentArrow);
            }
        }

        if (Input.GetKey(activationKey))
        {
            this.transform.localScale = pressedScale;
        } else
        {
            this.transform.localScale = initialScale;
        }
    }

    public void AddArrow(GameObject arrow)
    {
        if (this.arrowQueue.Count == 0)
        {
            this.currentArrow = arrow;
        }
        this.arrowQueue.Enqueue(arrow);
    }

    private void ProcessArrowActivation(GameObject arrow)
    {
        SuccessLevel successLevel = this.GetArrowSuccessLevel(arrow);
        
        if (successLevel == SuccessLevel.Perfect)
        {
            this.ProcessArrowPerfectHit(arrow);
        }
        if (successLevel == SuccessLevel.Good)
        {
            this.ProcessArrowGoodHit(arrow);
        }
        if (successLevel == SuccessLevel.Early || successLevel == SuccessLevel.Late)
        {
            this.ProcessArrowMiss(arrow);
        }
    }

    private SuccessLevel GetArrowSuccessLevel(GameObject arrow)
    {
        float arrowY = arrow.transform.position.y;
        float hitZoneY = this.transform.position.y;
        float arrowDistance = Mathf.Abs(arrowY - hitZoneY);

        bool isArrowBelowHitZone = arrowY <= hitZoneY - HitZone.GOOD_HIT_MAX_DISTANCE;
        bool isArrowAboveHitZone = arrowY >= hitZoneY + HitZone.GOOD_HIT_MAX_DISTANCE;

        if (arrowDistance <= HitZone.PERFECT_HIT_MAX_DISTANCE)
        {
            return HitZone.SuccessLevel.Perfect;
        }
        else if (arrowDistance <= HitZone.GOOD_HIT_MAX_DISTANCE)
        {
            return HitZone.SuccessLevel.Good;
        }
        else if (isArrowBelowHitZone && arrowDistance <= HitZone.EARLY_MISS_MAX_DISTANCE)
        {
            return HitZone.SuccessLevel.Early;
        }
        else if (isArrowAboveHitZone && arrowDistance <= HitZone.LATE_MISS_MAX_DISTANCE)
        {
            return HitZone.SuccessLevel.Late;
        }
        else
        {
            return HitZone.SuccessLevel.HasNotReachedHitZone;
        }
    }

    private void ProcessArrowPerfectHit(GameObject arrow)
    {
        this.DeleteHitArrow(arrow);
    }

    private void ProcessArrowGoodHit(GameObject arrow)
    {
        this.DeleteHitArrow(arrow);
    }

    private void ProcessArrowMiss(GameObject arrow)
    {
        this.DeleteMissedArrow(arrow);
    }

    private void DeleteHitArrow(GameObject arrow)
    {
        Destroy(arrow);
        this.arrowQueue.Dequeue();
        if (this.arrowQueue.Count > 0)
        {
            this.currentArrow = this.arrowQueue.Peek();
        }
        else
        {
            this.currentArrow = null;
        }
    }

    private void DeleteMissedArrow(GameObject arrow)
    {
        Destroy(arrow, HitZone.MISSED_ARROW_DESTROY_DELAY);
        this.DarkenArrowSprite(arrow);
        this.arrowQueue.Dequeue();
        if (this.arrowQueue.Count > 0)
        {
            this.currentArrow = this.arrowQueue.Peek();
        }
        else
        {
            this.currentArrow = null;
        }
    }

    private void DarkenArrowSprite(GameObject arrow)
    {
        SpriteRenderer spriteRenderer = arrow.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.r -= 0.5f;
        color.g -= 0.5f;
        color.b -= 0.5f;
        spriteRenderer.color = color;
    }

    private bool IsArrowPastHitZone(GameObject arrow)
    {
        float arrowY = arrow.transform.position.y;
        float hitZoneY = this.transform.position.y;
        return arrowY >= hitZoneY + HitZone.LATE_MISS_MAX_DISTANCE;
    }
}
