using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{
    public enum Owner
    {
        NoOwner,
        PlayerOne,
        PlayerTwo,
    }

    public bool isRenderedZone = false;
    public bool isRenderingAttackOnly = false;
    public float renderedCardScale = 1f;
    public bool hasMaximumCardLimit;
    public int maximumNumberOfCards;

    private Owner owner;
    private List<GameObject> cardObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.cardObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isRenderedZone)
        {
            this.HideCards(this.cardObjects);
        }
        else
        {
            if (this.isRenderingAttackOnly)
            {
                this.HideDefenseHalfOfCards(this.cardObjects);
            }
            this.MoveCardsToRenderedPositions(this.cardObjects);
        }
    }

    public int GetCardCount()
    {
        return this.cardObjects.Count;
    }

    public void AddCard(GameObject cardObject)
    {
        this.cardObjects.Add(cardObject);
    }

    public void AddAllCards(GameObject[] cardObjects)
    {
        this.cardObjects.AddRange(cardObjects);
    }

    public bool IsFull()
    {
        if (this.hasMaximumCardLimit)
        {
            return this.cardObjects.Count >= this.maximumNumberOfCards;
        }
        else
        {
            return false;
        }
        
    }

    public bool IsEmpty()
    {
        return this.cardObjects.Count == 0;
    }

    public GameObject DrawCard()
    {
        GameObject topCard = this.cardObjects[0];
        this.cardObjects.Remove(topCard);
        return topCard;
    }

    public void Shuffle()
    {
        List<GameObject> newCards = new List<GameObject>();
        while (this.cardObjects.Count > 0)
        {
            int oldIndex = Random.Range(0, this.cardObjects.Count);
            GameObject cardObject = this.cardObjects[oldIndex];
            this.cardObjects.Remove(cardObject);
            newCards.Add(cardObject);
        }
        this.cardObjects = newCards;
    }

    public void ShuffleAllIntoZone(CardZone destinationZone)
    {
        this.Shuffle();
        destinationZone.AddAllCards(this.cardObjects.ToArray());
        this.cardObjects.Clear();
    }

    private void HideCards(List<GameObject> cardObjects)
    {
        foreach (GameObject cardObject in cardObjects)
        {
            Card card = cardObject.GetComponent<Card>();
            card.HideAll();
        }
    }

    private void HideDefenseHalfOfCards(List<GameObject> cardObjects)
    {
        foreach (GameObject cardObject in cardObjects)
        {
            Card card = cardObject.GetComponent<Card>();
            card.HideDefense();
        }
    }

    private void MoveCardsToRenderedPositions(List<GameObject> cardObjects)
    {
        Vector3 nextPosition = this.transform.position;
        foreach (GameObject cardObject in cardObjects)
        {
            cardObject.transform.position = nextPosition;
            cardObject.transform.localScale = new Vector3(renderedCardScale, renderedCardScale, renderedCardScale);
            float width = this.GetCardObjectWidth(cardObject);
            nextPosition += new Vector3(width, 0, 0);
        }
    }

    private float GetCardObjectWidth(GameObject cardObject)
    {
        SpriteRenderer[] spriteRenderers = cardObject.GetComponentsInChildren<SpriteRenderer>();
        float largestSpriteWidth = 0f;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float spriteWidth = spriteRenderer.bounds.size.x;
            if (spriteWidth > largestSpriteWidth)
            {
                largestSpriteWidth = spriteWidth;
            }
        }
        return largestSpriteWidth;
    }
}
