using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title2 : MonoBehaviour
{
    public GameObject title3;

    // Start is called before the first frame update
    void Start()
    {
        title3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleAnimationEnd()
    {
        Debug.Log("hello");
        title3.SetActive(true);
    }
}
