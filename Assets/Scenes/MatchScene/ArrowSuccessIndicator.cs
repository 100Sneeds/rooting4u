using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSuccessIndicator : MonoBehaviour
{
    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite missSprite;

    private static float IDLE_DURATION_SECONDS = 2.5f;

    private SpriteRenderer spriteRenderer;
    private float idleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.idleTimer += Time.deltaTime;
        if (this.idleTimer > IDLE_DURATION_SECONDS)
        {
            this.Hide();
        }
        else
        {
            this.Show();
        }
    }

    public void ShowArrowSuccessResult(HitZone.SuccessLevel successLevel)
    {
        this.idleTimer = 0;
        if (successLevel == HitZone.SuccessLevel.Perfect)
        {
            this.spriteRenderer.sprite = perfectSprite;
        }
        if (successLevel == HitZone.SuccessLevel.Good)
        {
            this.spriteRenderer.sprite = goodSprite;
        }
        if (successLevel == HitZone.SuccessLevel.Late)
        {
            this.spriteRenderer.sprite = missSprite;
        }
    }

    private void Show()
    {
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    private void Hide()
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }
}
