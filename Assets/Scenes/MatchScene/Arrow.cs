using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float speed = ArrowSpawner.RHYTHM_BOARD_SPEED;

    public Sprite missSprite;

    public enum Direction
    {
        Left,
        Down,
        Up,
        Right
    }

    public enum SuccessState {
        HasNotReachedHitZone,
        Miss,
        Hit,
    }

    private SuccessState successState = SuccessState.HasNotReachedHitZone;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject child = this.transform.Find("ArrowSprite").gameObject;
        spriteRenderer = child.GetComponent<SpriteRenderer>();
        animator = child.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public SuccessState GetSuccessState()
    {
        return this.successState;
    }

    public void SetSuccessState(SuccessState successState)
    {
        this.successState = successState;
        if (successState == SuccessState.Hit)
        {
            this.PlayHitAnimation();
        }
        if (successState == SuccessState.Miss)
        {
            this.PlayMissAnimation();
        }
    }

    private void PlayHitAnimation()
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    private void PlayMissAnimation()
    {
        Color color = spriteRenderer.color;
        color.r -= 0.5f;
        color.g -= 0.5f;
        color.b -= 0.5f;
        spriteRenderer.color = color;
        spriteRenderer.sprite = missSprite;
        animator.Play("MissShake");
    }
}
