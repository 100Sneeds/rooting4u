using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public CardZone hand;
    public MatchPlayer owner;
    public KeyCode cursorLeftKey;
    public KeyCode cursorRightKey;
    public KeyCode selectCardKey;

    private int cursorIndex = 0;
    private List<int> selectedIndices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleKeyDownEvents();
        this.UpdatePosition();
    }

    private void HandleKeyDownEvents()
    {
        if (Input.GetKeyDown(this.cursorLeftKey))
        {
            this.MoveCursorIndexLeft();
        }
        else if (Input.GetKeyDown(this.cursorRightKey))
        {
            this.MoveCursorIndexRight();
        }
        else if (Input.GetKeyDown(this.selectCardKey))
        {

        }
    }

    private void MoveCursorIndexLeft()
    {
        int numCardsInHand = this.hand.GetCardCount();
        if (this.cursorIndex == 0)
        {
            this.cursorIndex = numCardsInHand - 1;
        }
        else
        {
            this.cursorIndex--;
            if (this.cursorIndex >= numCardsInHand)
            {
                this.cursorIndex = numCardsInHand - 1;
            }
        }
    }

    private void MoveCursorIndexRight()
    {
        int numCardsInHand = this.hand.GetCardCount();
        this.cursorIndex++;
        if (this.cursorIndex >= numCardsInHand)
        {
            this.cursorIndex = 0;
        }
    }

    private void UpdatePosition()
    {
        List<Vector3> renderedPositions = this.hand.GetRenderedPositions();
        if (renderedPositions.Count == 0)
        {
            return;
        }
        Vector3 cardPosition = renderedPositions[cursorIndex];
        this.transform.position = cardPosition + new Vector3(0, -0.62f, 0);
    }
}
