using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPhase : MonoBehaviour
{
    public MatchPlayer matchPlayerOne;
    public MatchPlayer matchPlayerTwo;
    private MatchStateController.PlayerSlot startingPlayerSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MatchStateController.PlayerSlot GetStartingPlayerSlot()
    {
        return this.startingPlayerSlot;
    }

    public void Setup()
    {
        this.startingPlayerSlot = DecideStartingPlayer();

        ShufflePlayerDeck(matchPlayerOne);
        ShufflePlayerDeck(matchPlayerTwo);

        DealPlayerHand(matchPlayerOne);
        DealPlayerHand(matchPlayerTwo);

        SetPlayerInitialStamina(matchPlayerOne);
        SetPlayerInitialStamina(matchPlayerTwo);
    }

    MatchStateController.PlayerSlot DecideStartingPlayer()
    {
        float randomValue = Random.Range(-1, 1);
        if (randomValue < 0)
        {
            return MatchStateController.PlayerSlot.PlayerOne;
        }
        else
        {
            return MatchStateController.PlayerSlot.PlayerTwo;
        }
    }

    void ShufflePlayerDeck(MatchPlayer player)
    {
        // stub
    }

    void DealPlayerHand(MatchPlayer player)
    {
        // stub
    }

    void SetPlayerInitialStamina(MatchPlayer player)
    {
        player.currentStamina = player.startingStamina;
    }
}
