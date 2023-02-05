using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballPlayerSprite : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetColor(Color color)
    {
        RuntimeAnimatorController animatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
        spriteRenderer.color = new Color(color.r, color.g, color.b);
        animator.runtimeAnimatorController = animatorController;
    }
}
