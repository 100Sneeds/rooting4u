using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    public CardSpawner cardSpawner;

    private PersistentCard[] persistentDeck;

    // Start is called before the first frame update
    void Start()
    {
        this.persistentDeck = this.GetStartingDeck();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject[] GetMatchDeck()
    {
        if (this.persistentDeck == null)
        {
            this.persistentDeck = this.GetStartingDeck();
        }
        List<GameObject> matchDeck = new List<GameObject>();
        foreach (PersistentCard persistentCard in this.persistentDeck)
        {
            GameObject cardObject = this.cardSpawner.CreateCardObject(persistentCard);
            matchDeck.Add(cardObject);
        }
        return matchDeck.ToArray();
    }

    private PersistentCard[] GetStartingDeck()
    {
        return new PersistentCard[]
        {
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),

            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
            new PersistentCard(1, CardColor.Red, 1, CardColor.Red, 1),
        };
    }
}
