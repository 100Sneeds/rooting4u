using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer : MonoBehaviour
{
    public PlayerSlot playerSlot;

    public int baseStaminaRegeneration = 1;
    public int startingStamina = 3;
    public int currentStamina = 0;

    public CardZone deck;
    public CardZone hand;
    public CardZone discard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCardsUntilHandFull()
    {
        while (!hand.IsFull())
        {
            if (deck.IsEmpty())
            {
                discard.ShuffleAllIntoZone(deck);
            }
            GameObject drawnCard = deck.DrawCard();
            hand.AddCard(drawnCard);
        }
    }
}
