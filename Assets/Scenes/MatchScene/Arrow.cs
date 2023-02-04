using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static float SCROLL_SPEED = 3f;

    public enum Direction
    {
        Left,
        Down,
        Up,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, Arrow.SCROLL_SPEED * Time.deltaTime, 0);
    }
}
