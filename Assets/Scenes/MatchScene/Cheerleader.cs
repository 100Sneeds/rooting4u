using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;

public class Cheerleader : MonoBehaviour
{
    public enum Pose
    {
        Thinking,
        Cheering,
    }

    public Pose pose = Pose.Thinking;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.pose == Pose.Thinking)
        {
            animator.Play("Thinking");
        }
        if (this.pose == Pose.Cheering)
        {
            animator.Play("Cheering");
        }
    }
}
