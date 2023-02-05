using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardZone
{
    public float renderedMarkerScale = 1f;
    public HandCursor cursor;
    public MatchPlayer owner;
    public HandSelectionMarker selectionMarker;

    private static float SELECTED_CARD_Y_OFFSET = 0.2f;

    private List<int> selectedIndices = new List<int>();
    private List<HandSelectionMarker> selectionMarkers;

    // Start is called before the first frame update
    void Start()
    {
        this.selectionMarkers = this.GetSelectionMarkers();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isRenderedZone)
        {
            this.ShowCards(this.cardObjects);
        }
        this.MoveCardsToRenderedPositions(this.cardObjects);
    }

    public void SelectCardAtIndex(int index)
    {
        if (selectedIndices.Contains(index))
        {
            selectedIndices.Remove(index);
            owner.currentStamina += this.GetStaminaCostOfCardAtIndex(index);
        }
        else
        {
            if (IsCardAtIndexAffordable(index))
            {
                selectedIndices.Add(index);
                owner.currentStamina -= this.GetStaminaCostOfCardAtIndex(index);
            }
        }
    }

    public bool IsCardAtIndexAffordable(int index)
    {
        GameObject cardObject = this.cardObjects[index];
        Card card = cardObject.GetComponent<Card>();

        int ownerCurrentStamina = this.owner.currentStamina;
        int cardCost = card.staminaCost;

        return ownerCurrentStamina >= cardCost;
    }

    public List<GameObject> GetSelectedCards()
    {
        List<GameObject> selectedCards = new List<GameObject>();
        foreach (int cardIndex in this.selectedIndices)
        {
            selectedCards.Add(cardObjects[cardIndex]);
        }
        return selectedCards;
    }

    public List<GameObject> GetAffordableCards()
    {
        List<GameObject> affordableCards = new List<GameObject>();
        foreach (GameObject cardObject in this.cardObjects)
        {
            Card card = cardObject.GetComponent<Card>();
            if (card.staminaCost <= owner.currentStamina)
            {
                affordableCards.Add(cardObject);
            }
        }
        return affordableCards;
    }

    public void ClearSelection()
    {
        this.selectedIndices.Clear();
    }

    private List<HandSelectionMarker> GetSelectionMarkers()
    {
        List<HandSelectionMarker> selectionMarkers = new List<HandSelectionMarker>();
        for (int index = 0; index < this.maximumNumberOfCards; index++)
        {
            HandSelectionMarker marker = Instantiate(this.selectionMarker, Vector3.zero, Quaternion.identity);
            marker.transform.localScale = new Vector3(renderedMarkerScale, renderedMarkerScale, renderedMarkerScale);
            selectionMarkers.Add(marker);
        }
        return selectionMarkers;
    }

    private void MoveCardsToRenderedPositions(List<GameObject> cardObjects)
    {
        float nextPositionX = this.transform.position.x;
        for (int cardIndex = 0; cardIndex < cardObjects.Count; cardIndex++)
        {
            GameObject cardObject = cardObjects[cardIndex];
            HandSelectionMarker marker = this.selectionMarkers[cardIndex];

            bool isSelectedIndex = this.selectedIndices.Contains(cardIndex);
            float cardPositionOffsetY = isSelectedIndex ? SELECTED_CARD_Y_OFFSET : 0f;
            float cardPositionY = this.transform.position.y + cardPositionOffsetY;

            Card card = cardObject.GetComponent<Card>();
            card.SetDestinationPosition(new Vector3(nextPositionX, cardPositionY, 0), 0.1f);

            float markerPositionY = this.GetCardObjectTop(cardObject);
            marker.transform.position = new Vector3(nextPositionX, markerPositionY, 0);
            if (isSelectedIndex)
            {
                marker.Show();
                marker.slotNumber = this.selectedIndices.IndexOf(cardIndex) + 1;
            }
            else
            {
                marker.Hide();
            }
            
            cardObject.transform.localScale = new Vector3(renderedCardScale, renderedCardScale, renderedCardScale);

            float width = this.GetCardObjectWidth(cardObject);
            nextPositionX += width;
        }
    }

    private int GetStaminaCostOfCardAtIndex(int index)
    {
        GameObject cardObject = this.cardObjects[index];
        Card card = cardObject.GetComponent<Card>();
        return card.staminaCost;
    }
}
