using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTurnPhase : MonoBehaviour
{
    public MatchStateController matchStateController;
    public MatchPlayer owner;
    public CardZone defenseZone;

    public KeyCode confirmCardSelectKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (matchStateController.GetStartingPlayerSlot() == owner.playerSlot)
        {
            if (Input.GetKeyDown(confirmCardSelectKey))
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
