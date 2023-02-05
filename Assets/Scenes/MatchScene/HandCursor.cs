using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public MatchStateController matchStateController;
    public Hand hand;
    public MatchPlayer owner;
    public KeyCode cursorLeftKey;
    public KeyCode cursorRightKey;
    public KeyCode selectCardKey;

    public Sprite idleSprite;
    public Sprite selectSprite;

    private int cursorIndex = 0;

    public bool isAI;
    public float AIActionDelay = 0.5f;

    public bool isActive;
    public Stack<int> cardIndexStack;  // Stack of cards to select
    private bool wasLastFrameActive;

    public bool isAiCursorSelectionDone = false;

    // Start is called before the first frame update
    void Start()
    {
        cardIndexStack = new Stack<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAI) {
            AIUpdate();
        } else {
            PlayerUpdate();
        }
    }

    private void AIUpdate() {
        isActive = this.GetIsActive();
        // If the last frame was not active, we come up with a series of commands
        if (!wasLastFrameActive){
            isAiCursorSelectionDone = false;
            cardIndexStack = new Stack<int>();
            
            List<int> indexList = new List<int> {0, 1, 2};
            // Shuffle the list
            for (int i = 0; i < indexList.Count - 1; i++)
            {
                int temp = indexList[i];
                int rand = Random.Range(i, indexList.Count);
                indexList[i] = indexList[rand];
                indexList[rand] = temp;
            }

            cardIndexStack.Push(indexList[0]);
            cardIndexStack.Push(indexList[1]);
            cardIndexStack.Push(indexList[2]);

        }
        this.UpdatePosition();
        if (isActive && cardIndexStack.Count != 0) {
            AIActionDelay -= Time.deltaTime;
            if(AIActionDelay < 0) {
                int i = cardIndexStack.Pop();
                // Move the hand to the right index
                while (i != cursorIndex){
                    this.MoveCursorIndexRight();
                }

                this.hand.SelectCardAtIndex(i);
                AIActionDelay = 0.5f;
                
            }
            if (cardIndexStack.Count == 0)
            {
                this.isAiCursorSelectionDone = true;
            }
        }
        wasLastFrameActive = isActive;
    }

    private void PlayerUpdate() {
        bool isActive = this.GetIsActive();

        if (isActive)
        {
            this.Show();
            this.HandleKeyDownEvents();
            this.UpdatePosition();
        }
        else
        {
            this.Hide();
            this.cursorIndex = 0;
        }
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
            this.hand.SelectCardAtIndex(this.cursorIndex);
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Input.GetKey(this.selectCardKey))
        {
            spriteRenderer.sprite = selectSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
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

    private bool GetIsActive()
    {
        bool isOwnerPlayerOne = this.owner.playerSlot == PlayerSlot.PlayerOne;
        bool isOwnerPlayerTwo = !isOwnerPlayerOne;

        bool isFirstTurn = matchStateController.currentMatchState == MatchState.FirstTurn;
        bool isPlayerOneFirstTurn = isFirstTurn && matchStateController.GetStartingPlayerSlot() == PlayerSlot.PlayerOne;
        bool isPlayerTwoFirstTurn = isFirstTurn && matchStateController.GetStartingPlayerSlot() == PlayerSlot.PlayerTwo;

        bool isPlayerOnePreparing = matchStateController.currentMatchState == MatchState.PlayerTwoPerform;
        bool isPlayerTwoPreparing = matchStateController.currentMatchState == MatchState.PlayerOnePerform;

        bool isPlayerOneChoosingCards = isPlayerOneFirstTurn || isPlayerOnePreparing;
        bool isPlayerTwoChoosingCards = isPlayerTwoFirstTurn || isPlayerTwoPreparing;

        return (isOwnerPlayerOne && isPlayerOneChoosingCards) || (isOwnerPlayerTwo && isPlayerTwoChoosingCards);
    }

    private void Hide()
    {
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    private void Show()
    {
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }
}
