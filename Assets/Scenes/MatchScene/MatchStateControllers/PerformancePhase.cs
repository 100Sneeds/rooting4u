using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformancePhase : MonoBehaviour
{
    public MatchStateController matchStateController;
    public MatchPlayer performingPlayer;
    public MatchPlayer preparingPlayer;
    public ArrowSpawner arrowSpawner;
    public CardZone attackZone;
    public CardZone defenseZone;
    public ProgressBar progressBar;

    private int defenseZoneCardIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformancePhaseInit()
    {
        this.defenseZoneCardIndex = 0;
        List<GameObject> defenseCards = this.GetDefenseCards();
        List<NoteSequence> sequencesForCards = this.GetSequencesFromCards(defenseCards);
        this.AddSequencesToQueue(sequencesForCards);
        this.preparingPlayer.currentStamina += this.preparingPlayer.baseStaminaRegeneration;
    }

    public void HandleSequenceResult(NoteSequence.SuccessState successState)
    {
        List<GameObject> defenseCards = this.GetDefenseCards();
        GameObject defenseCard = defenseCards[this.defenseZoneCardIndex];
        this.ModifyCardBySuccessState(defenseCard, successState);
        if (successState == NoteSequence.SuccessState.FullCombo || successState == NoteSequence.SuccessState.Success)
        {
            this.performingPlayer.currentStamina += 1;
        }
        defenseZoneCardIndex++;

        if (defenseZoneCardIndex == defenseCards.Count)
        {
            this.ResolveDamagePhase();
        }
    }

    private void AddSequencesToQueue(List<NoteSequence> sequences)
    {
        foreach (NoteSequence sequence in sequences)
        {
            this.arrowSpawner.AddSequenceToQueue(sequence);
        }
    }

    private List<GameObject> GetDefenseCards()
    {
        return this.defenseZone.GetCards();
    }

    private List<NoteSequence> GetSequencesFromCards(List<GameObject> cardObjects)
    {
        List<NoteSequence> sequences = new List<NoteSequence>();
        foreach (GameObject cardObject in cardObjects)
        {
            Card card = cardObject.GetComponent<Card>();
            NoteSequence sequence = NoteSequenceGenerator.GetNoteSequenceOfCard(card);
            sequences.Add(sequence);
        }
        return sequences;
    }

    private void ModifyCardBySuccessState(GameObject cardObject, NoteSequence.SuccessState successState)
    {
        Card card = cardObject.GetComponent<Card>();
        if (successState == NoteSequence.SuccessState.FullCombo)
        {
            card.boostedState = Card.BoostedState.PerformanceBoosted;
        }
        if (successState == NoteSequence.SuccessState.Fail)
        {
            card.boostedState = Card.BoostedState.PerformancePenalized;
        }
    }

    private void ResolveDamagePhase()
    {
        // do all damage resolution
        this.ResolveDamage();
        MatchState nextMatchStateByDamage = this.GetNextMatchStateByDamage();
        this.matchStateController.currentMatchState = nextMatchStateByDamage;

        // move all cards
        this.MoveAttackCardsToDiscard();
        this.MoveDefenseCardsToAttackZone();
        this.MoveSelectedCardsToDefenseZone(); // can mutate match state to game end if preparing player cannot play any cards
    }

    private void MoveAttackCardsToDiscard()
    {
        List<GameObject> attackCardObjects = this.attackZone.GetCards();
        foreach (GameObject attackCardObject in attackCardObjects)
        {
            Card attackCard = attackCardObject.GetComponent<Card>();
            attackCard.boostedState = Card.BoostedState.NoBoost;
        }
        preparingPlayer.discard.AddAllCards(attackCardObjects.ToArray());
        this.attackZone.ClearCards();
    }

    private void MoveDefenseCardsToAttackZone()
    {
        List<GameObject> defenseCardObject = this.defenseZone.GetCards();
        attackZone.AddAllCards(defenseCardObject.ToArray());
        defenseZone.ClearCards();
    }

    private void MoveSelectedCardsToDefenseZone()
    {
        List<GameObject> selectedCards = preparingPlayer.hand.GetSelectedCards();
        if (selectedCards.Count == 0)
        {
            List<GameObject> affordableCards = preparingPlayer.hand.GetAffordableCards();
            if (affordableCards.Count == 0)
            {
                this.matchStateController.currentMatchState = MatchState.GameEnd;
                return;
            }
            selectedCards = new List<GameObject>();
            selectedCards.Add(affordableCards[0]);
        }

        foreach (GameObject selectedCard in selectedCards)
        {
            preparingPlayer.hand.RemoveCard(selectedCard);
        }
        defenseZone.AddAllCards(selectedCards.ToArray());
        preparingPlayer.DrawCardsUntilHandFull();
    }

    private MatchState GetNextMatchStateByDamage()
    {
        if (this.progressBar.IsGameWonByScore())
        {
            return MatchState.GameEnd;
        }
        if (this.performingPlayer.playerSlot == PlayerSlot.PlayerOne)
        {
            return MatchState.PlayerTwoPerformInit;
        }
        return MatchState.PlayerOnePerformInit;
    }

    private void ResolveDamage()
    {
        int damage = this.GetDamage();
        PlayerSlot playerTakingDamage = this.performingPlayer.playerSlot == PlayerSlot.PlayerOne ?
            PlayerSlot.PlayerOne :
            PlayerSlot.PlayerTwo;

        if (playerTakingDamage == PlayerSlot.PlayerOne)
        {
            this.progressBar.score += damage;
        }
        else
        {
            this.progressBar.score -= damage;
        }
    }

    private int GetDamage()
    {
        List<GameObject> attackCardObjects = this.attackZone.GetCards();
        int totalDamage = 0;
        for (int attackCardIndex = 0; attackCardIndex < attackCardObjects.Count; attackCardIndex++)
        {
            totalDamage += GetDamageForSlot(attackCardIndex);
        }
        return totalDamage;
    }

    private int GetDamageForSlot(int slotIndex)
    {
        List<GameObject> attackCardObjects = this.attackZone.GetCards();
        List<GameObject> defenseCardObjects = this.defenseZone.GetCards();

        GameObject attackCardObject = attackCardObjects[slotIndex];
        Card attackCard = attackCardObject.GetComponent<Card>();

        bool isDefenderAvailable = defenseCardObjects.Count > slotIndex;
        if (isDefenderAvailable)
        {
            Card defenseCard = defenseCardObjects[slotIndex].GetComponent<Card>();
            return Card.CalculateDamageToDefender(attackCard, defenseCard);
        }
        else
        {
            return attackCard.GetModifiedAttackValue();
        }
    }
}
