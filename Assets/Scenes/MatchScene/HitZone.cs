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

    private Vector3 initialScale;
    private Vector3 pressedScale;

    // AI player related Stuff
    public bool isAI;
    public PlayerAI playerAI;
    public int difficultyAI;
    private Queue<GameObject> arrowQueue = new Queue<GameObject>();
    private GameObject currentArrow;
    private GameObject lastArrow;
    public ComboCounter comboCounter;
    public ArrowSuccessIndicator arrowSuccessIndicator;
    public ArrowPow arrowPow;

    // Start is called before the first frame update
    void Start()
    {
        
        initialScale = this.transform.localScale;
        pressedScale = new Vector3(
            initialScale.x * HitZone.PRESSED_SPRITE_SCALING_FACTOR,
            initialScale.y * HitZone.PRESSED_SPRITE_SCALING_FACTOR,
            initialScale.z * HitZone.PRESSED_SPRITE_SCALING_FACTOR
        );
        playerAI = new PlayerAI(difficulty: difficultyAI);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAI) {
            AIUpdate();
        } else {
            PlayerUpdate();
        }
        
    }
    void AIUpdate() {
        this.transform.localScale = initialScale;
        // handle when note goes beyond the hit zone
        if (this.currentArrow != null && this.IsArrowPastHitZone(this.currentArrow)) {
            this.ProcessArrowMiss(this.currentArrow);
        }

        if (this.currentArrow != null) {
            float noteDistance = this.transform.position.y - currentArrow.transform.position.y;
            if(noteDistance <= this.playerAI.nextHitTiming){
                this.ProcessArrowActivation(this.currentArrow);
                this.transform.localScale = pressedScale;
                // generate next note hit timing
                this.playerAI.generateNextHitTiming(1);
            }
        }
    }

    void PlayerUpdate(){
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
        HitZone.SuccessLevel successLevel = this.GetArrowSuccessLevel(arrow);
        
        if (successLevel == HitZone.SuccessLevel.Perfect)
        {
            this.ProcessArrowPerfectHit(arrow);
            comboCounter.incrementCombo();
        }
        if (successLevel == HitZone.SuccessLevel.Good)
        {
            this.ProcessArrowGoodHit(arrow);
            comboCounter.incrementCombo();
        }
        if (successLevel == HitZone.SuccessLevel.Early || successLevel == HitZone.SuccessLevel.Late)
        {
            this.ProcessArrowMiss(arrow);
        }
    }

    private HitZone.SuccessLevel GetArrowSuccessLevel(GameObject arrow)
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
        this.arrowSuccessIndicator.ShowArrowSuccessResult(SuccessLevel.Perfect);
        Instantiate(this.arrowPow, this.transform.position, Quaternion.identity);
    }

    private void ProcessArrowGoodHit(GameObject arrow)
    {
        this.DeleteHitArrow(arrow);
        this.arrowSuccessIndicator.ShowArrowSuccessResult(SuccessLevel.Good);
        Instantiate(this.arrowPow, this.transform.position, Quaternion.identity);
    }

    private void ProcessArrowMiss(GameObject arrow)
    {
        this.DeleteMissedArrow(arrow);
        this.arrowSuccessIndicator.ShowArrowSuccessResult(SuccessLevel.Late);
        comboCounter.resetCombo();
    }

    private void DeleteHitArrow(GameObject arrowObject)
    {
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.successState = Arrow.SuccessState.Hit;
        this.HideArrowSprite(arrowObject);
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

    private void DeleteMissedArrow(GameObject arrowObject)
    {
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.successState = Arrow.SuccessState.Miss;
        this.DarkenArrowSprite(arrowObject);
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

    private void HideArrowSprite(GameObject arrow)
    {
        SpriteRenderer spriteRenderer = arrow.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    private bool IsArrowPastHitZone(GameObject arrow)
    {
        float arrowY = arrow.transform.position.y;
        float hitZoneY = this.transform.position.y;
        return arrowY >= hitZoneY + HitZone.LATE_MISS_MAX_DISTANCE;
    }
}
