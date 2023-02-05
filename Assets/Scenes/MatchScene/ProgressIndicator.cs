using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressIndicator : MonoBehaviour
{
    public ProgressBar progressBar;

    private static float X_OFFSET_PER_POINT = 0.97f;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.GetPosition();
    }

    private Vector3 GetPosition()
    {
        float originalX = originalPosition.x;
        float newX = originalX + progressBar.score * X_OFFSET_PER_POINT;
        return new Vector3(newX, originalPosition.y, originalPosition.z);
    }
}
