using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressIndicator : MonoBehaviour
{
    public ProgressBar progressBar;

    private static float X_OFFSET_PER_POINT = 0.97f;
    private static float POSITION_LERP_DURATION_SECONDS = 0.5f;

    private Vector3 originalPosition;

    private Vector3 sourceVector;
    private Vector3 destinationVector;
    private float lerpTimer = 0;
    private int previousScore;

    // Start is called before the first frame update
    void Start()
    {
        previousScore = this.progressBar.GetScore();
        originalPosition = this.transform.position;
        sourceVector = originalPosition;
        destinationVector = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTimer += Time.deltaTime;

        int currentScore = this.progressBar.GetScore();
        bool isScoreChanged = currentScore != previousScore;
        previousScore = currentScore;

        if (isScoreChanged)
        {
            sourceVector = this.transform.position;
            destinationVector = this.GetDestinationPositionForScore(currentScore);
            lerpTimer = 0;
        }

        float lerpPercent = lerpTimer / POSITION_LERP_DURATION_SECONDS;
        this.transform.position = Vector3.Lerp(sourceVector, destinationVector, lerpPercent);
    }

    private Vector3 GetDestinationPositionForScore(int score)
    {
        float originalX = originalPosition.x;
        float newX = originalX + score * X_OFFSET_PER_POINT;
        return new Vector3(newX, originalPosition.y, originalPosition.z);
    }
}
