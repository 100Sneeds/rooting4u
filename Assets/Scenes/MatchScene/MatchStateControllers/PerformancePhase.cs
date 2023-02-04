using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformancePhase : MonoBehaviour
{
    public MatchStateController matchStateController;
    public MatchPlayer owner;
    public ArrowSpawner arrowSpawner;
    public CardZone defenseZone;

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
    }

    public void HandleSequenceResult(NoteSequence.SuccessState successState)
    {
        List<GameObject> defenseCards = this.GetDefenseCards();
        GameObject defenseCard = defenseCards[this.defenseZoneCardIndex];
        this.ModifyCardBySuccessState(defenseCard, successState);
        if (successState == NoteSequence.SuccessState.FullCombo || successState == NoteSequence.SuccessState.Success)
        {
            this.owner.currentStamina += 1;
        }
        defenseZoneCardIndex++;
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
}
