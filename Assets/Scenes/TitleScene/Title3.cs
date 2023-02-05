using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title3 : MonoBehaviour
{
    public GameObject title4;
    public GameObject title5;

    // Start is called before the first frame update
    void Start()
    {
        title4.SetActive(false);
        title5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleAnimationEnd()
    {
        title4.SetActive(true);
        title5.SetActive(true);
    }
}
