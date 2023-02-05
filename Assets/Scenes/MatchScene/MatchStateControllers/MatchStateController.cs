using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStateController : MonoBehaviour
{
    public static MatchState INITIAL_MATCH_STATE = MatchState.Setup;
    public MatchState currentMatchState = MatchStateController.INITIAL_MATCH_STATE;

    public SetupPhase setupPhase;
    public PerformancePhase performancePhaseOne;
    public PerformancePhase performancePhaseTwo;

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
                this.currentMatchState = MatchState.FirstTurn;
                break;
            case MatchState.FirstTurn:
                // run first turn
                // controlled by first turn phase objects
                break;
            case MatchState.PlayerOnePerformInit:
                performancePhaseOne.PerformancePhaseInit();
                this.currentMatchState = MatchState.PlayerOnePerform;
                break;
            case MatchState.PlayerOnePerform:
                // run player one perform
                break;
            case MatchState.PlayerTwoPerformInit:
                performancePhaseTwo.PerformancePhaseInit();
                this.currentMatchState = MatchState.PlayerTwoPerform;
                break;
            case MatchState.PlayerTwoPerform:
                // run player two perform
                break;
            case MatchState.GameEnd:
                // run game end
                break;
        }
    }

    public PlayerSlot GetStartingPlayerSlot()
    {
        return this.startingPlayerSlot;
    }
}
