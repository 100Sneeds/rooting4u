using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressIndicator : MonoBehaviour
{
    public ProgressBar progressBar;
    public FootballPlayerSprite footballPlayerLeft;
    public FootballPlayerSprite footballPlayerRight;

    private static float X_OFFSET_PER_POINT = 0.97f;
    private static float POSITION_LERP_DURATION_SECONDS = 1f;

    private Vector3 originalPosition;

    private Vector3 sourceVector;
    private Vector3 destinationVector;
    private float lerpTimer = 0;
    private int previousScore;

    private enum ProgressState {
        PushLeft,
        PushRight,
        Idle,
    }

    private ProgressState progressState = ProgressState.Idle;

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
            if (destinationVector.x > sourceVector.x)
            {
                this.progressState = ProgressState.PushRight;
            }
            if (destinationVector.x < sourceVector.x)
            {
                this.progressState = ProgressState.PushLeft;
            }
        }

        float lerpPercent = lerpTimer / POSITION_LERP_DURATION_SECONDS;
        this.transform.position = Vector3.Lerp(sourceVector, destinationVector, lerpPercent);

        if (lerpPercent >= 1.0f)
        {
            this.progressState = ProgressState.Idle;
        }

        this.UpdateFootballPlayerSprites(this.progressState);
    }

    private Vector3 GetDestinationPositionForScore(int score)
    {
        float originalX = originalPosition.x;
        float newX = originalX + score * X_OFFSET_PER_POINT;
        return new Vector3(newX, originalPosition.y, originalPosition.z);
    }

    private void UpdateFootballPlayerSprites(ProgressState progressState)
    {
        switch (progressState)
        {
            default:
            case ProgressState.Idle:
                this.footballPlayerLeft.GetAnimator().Play("Idle");
                this.footballPlayerRight.GetAnimator().Play("Idle");
				this.footballPlayerLeft.transform.position = this.transform.position + new Vector3(-0.8f,0,0);
				this.footballPlayerRight.transform.position = this.transform.position + new Vector3(0.8f,0,0);
                break;
            case ProgressState.PushRight:
                this.footballPlayerLeft.GetAnimator().Play("Pushing");
                this.footballPlayerRight.GetAnimator().Play("Pushed");
				this.footballPlayerLeft.transform.position = this.transform.position + new Vector3(-0.68f,0,0);
				this.footballPlayerRight.transform.position = this.transform.position + new Vector3(0.44f,0,0);
                break;
            case ProgressState.PushLeft:
                this.footballPlayerLeft.GetAnimator().Play("Pushed");
                this.footballPlayerRight.GetAnimator().Play("Pushing");
				this.footballPlayerLeft.transform.position = this.transform.position + new Vector3(-0.44f,0,0);
				this.footballPlayerRight.transform.position = this.transform.position + new Vector3(0.68f,0,0);
				break;
        }
    }
}
