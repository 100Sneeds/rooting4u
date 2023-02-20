using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTurnPhase : MonoBehaviour
{
    public MatchStateController matchStateController;
    public MatchPlayer owner;
    public CardZone defenseZone;

    public KeyCode confirmCardSelectKey;

    public HandCursor aiCursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (matchStateController.GetStartingPlayerSlot() == owner.playerSlot)
        {
            bool isAiDoneSelectingCards = aiCursor != null && aiCursor.isAiCursorSelectionDone;
            bool isHumanDoneSelectingCards = confirmCardSelectKey != KeyCode.None && Input.GetKeyDown(confirmCardSelectKey);

            if (matchStateController.currentMatchState == MatchState.FirstTurn && (isAiDoneSelectingCards || isHumanDoneSelectingCards))
            {
                this.ConfirmCardSelection();
            }
        }
    }

    private void ConfirmCardSelection()
    {
        List<GameObject> selectedCards = owner.hand.GetSelectedCards();
        if (selectedCards.Count == 0)
        {
            return;
        }

        foreach (GameObject selectedCard in selectedCards)
        {
            owner.hand.RemoveCard(selectedCard);
        }
        owner.hand.ClearSelection();
        defenseZone.AddAllCards(selectedCards.ToArray());
        owner.DrawCardsUntilHandFull();

        if (owner.playerSlot == PlayerSlot.PlayerOne)
        {
            this.matchStateController.currentMatchState = MatchState.PlayerOnePerformInit;
        }
        else
        {
            this.matchStateController.currentMatchState = MatchState.PlayerTwoPerformInit;
        }
    }
}
