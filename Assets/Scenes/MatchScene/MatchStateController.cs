using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStateController : MonoBehaviour
{
    public enum MatchState
    {
        Setup,
        FirstTurn,
        PlayerOnePerform,
        PlayerTwoPerform,
        GameEnd,
    }

    public enum PlayerSlot
    {
        PlayerOne, // left player
        PlayerTwo, // right player
    }

    public static MatchState INITIAL_MATCH_STATE = MatchState.Setup;
    public MatchState currentMatchState = MatchStateController.INITIAL_MATCH_STATE;

    public SetupPhase setupPhase;

    private PlayerSlot startingPlayerSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentMatchState) {
            case MatchState.Setup:
                setupPhase.Setup();
                this.startingPlayerSlot = setupPhase.GetStartingPlayerSlot();
                // delay for animations
                break;
            case MatchState.FirstTurn:
                // run first turn
                break;
            case MatchState.PlayerOnePerform:
                // run player one perform
                break;
            case MatchState.PlayerTwoPerform:
                // run player two perform
                break;
            case MatchState.GameEnd:
                // run game end
                break;
        }
    }
}
