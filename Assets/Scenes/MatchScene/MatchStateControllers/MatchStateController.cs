using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class MatchStateController : MonoBehaviour
{
    public static MatchState INITIAL_MATCH_STATE = MatchState.Setup;
    public MatchState currentMatchState = MatchStateController.INITIAL_MATCH_STATE;

    public SetupPhase setupPhase;
    public PerformancePhase performancePhaseOne;
    public PerformancePhase performancePhaseTwo;

    public ResultsWin resultsWin;
    public ResultsLoss resultsLoss;

    private PlayerSlot startingPlayerSlot;
    public PlayerSlot winningPlayerSlot = PlayerSlot.PlayerOne;
    public KeyCode playAgainKey;

    public Cheerleader cheerleaderOne;
    public Cheerleader cheerleaderTwo;

    public OpponentRandomizer opponentRandomizer;

    public FootballPlayerSprite footballPlayerLeft;
    public FootballPlayerSprite footballPlayerRight;

    // Start is called before the first frame update
    void Start()
    {
        this.resultsWin.gameObject.SetActive(false);
        this.resultsLoss.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentMatchState) {
            case MatchState.Setup:
                setupPhase.Setup();
                this.startingPlayerSlot = setupPhase.GetStartingPlayerSlot();
                this.SetPlayerColor();
                this.SetRandomOpponent();
                // TODO delay for animations
                this.currentMatchState = MatchState.FirstTurn;
                break;
            case MatchState.FirstTurn:
                // run first turn
                // controlled by first turn phase objects
                break;
            case MatchState.PlayerOnePerformInit:
                performancePhaseOne.PerformancePhaseInit();
                cheerleaderOne.pose = Cheerleader.Pose.Cheering;
                cheerleaderTwo.pose = Cheerleader.Pose.Thinking;
                this.currentMatchState = MatchState.PlayerOnePerform;
                break;
            case MatchState.PlayerOnePerform:
                // run player one perform
                // handled by performance phase objects
                break;
            case MatchState.PlayerTwoPerformInit:
                performancePhaseTwo.PerformancePhaseInit();
                cheerleaderTwo.pose = Cheerleader.Pose.Cheering;
                cheerleaderOne.pose = Cheerleader.Pose.Thinking;
                this.currentMatchState = MatchState.PlayerTwoPerform;
                break;
            case MatchState.PlayerTwoPerform:
                // run player two perform
                // handled by performance phase objects
                break;
            case MatchState.GameEnd:
                if (winningPlayerSlot == PlayerSlot.PlayerOne)
                {
                    this.resultsWin.gameObject.SetActive(true);
                }
                else
                {
                    this.resultsLoss.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(this.playAgainKey))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
        }
    }

    public PlayerSlot GetStartingPlayerSlot()
    {
        return this.startingPlayerSlot;
    }

    private void SetPlayerColor()
    {
        OpponentSkinData playerSkinData = opponentRandomizer.GetSkinData(OpponentSkin.Blue);
        footballPlayerLeft.SetColor(playerSkinData.GetColor());
    }

    private void SetRandomOpponent()
    {
        OpponentSkin randomOpponentSkin = opponentRandomizer.GetRandomOpponentSkin();
        OpponentSkinData opponentSkinData = opponentRandomizer.GetSkinData(randomOpponentSkin);
        SpriteLibraryAsset randomSpriteLibraryAsset = opponentSkinData.GetSpriteLibraryAsset();
        SpriteLibrary opponentSpriteLibrary = this.cheerleaderTwo.GetComponent<SpriteLibrary>();
        opponentSpriteLibrary.spriteLibraryAsset = randomSpriteLibraryAsset;
        footballPlayerRight.SetColor(opponentSkinData.GetColor());
    }
}
