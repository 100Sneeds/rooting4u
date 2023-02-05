using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPhase : MonoBehaviour
{
    public PersistentPlayer persistentPlayerOne;
    public PersistentPlayer persistentPlayerTwo;

    public MatchPlayer matchPlayerOne;
    public MatchPlayer matchPlayerTwo;

    public CoinFlip coinFlip;

    private PlayerSlot startingPlayerSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerSlot GetStartingPlayerSlot()
    {
        return this.startingPlayerSlot;
    }

    public void Setup()
    {
        this.startingPlayerSlot = DecideStartingPlayer();
        if (this.startingPlayerSlot == PlayerSlot.PlayerOne)
        {
            coinFlip.result = CoinFlip.Result.Home;
        }
        else
        {
            coinFlip.result = CoinFlip.Result.Away;
        }

        SetPlayerInitialDeck(matchPlayerOne, persistentPlayerOne);
        SetPlayerInitialDeck(matchPlayerTwo, persistentPlayerTwo);

        DealPlayerHand(matchPlayerOne);
        DealPlayerHand(matchPlayerTwo);

        SetPlayerInitialStamina(matchPlayerOne);
        SetPlayerInitialStamina(matchPlayerTwo);
    }

    private PlayerSlot DecideStartingPlayer()
    {
        return PlayerSlot.PlayerOne; // Set to human player for now
        float randomValue = Random.Range(-1, 1);
        if (randomValue < 0)
        {
            return PlayerSlot.PlayerOne;
        }
        else
        {
            return PlayerSlot.PlayerTwo;
        }
    }

    private void SetPlayerInitialDeck(MatchPlayer matchPlayer, PersistentPlayer persistentPlayer)
    {
        GameObject[] matchDeck = persistentPlayer.GetMatchDeck();
        matchPlayer.deck.AddAllCards(matchDeck);
        matchPlayer.deck.Shuffle();
    } 

    private void DealPlayerHand(MatchPlayer player)
    {
        player.DrawCardsUntilHandFull();
    }

    private void SetPlayerInitialStamina(MatchPlayer player)
    {
        player.currentStamina = player.startingStamina;
    }
}
