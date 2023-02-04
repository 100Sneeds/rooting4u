using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateCardObject(int attackValue, CardColor attackColor, int defenseValue, CardColor defenseColor, int staminaCost)
    {
        GameObject cardObject = Instantiate(this.cardObject, Vector3.zero, Quaternion.identity);
        Card card = cardObject.GetComponent<Card>();
        card.attackValue = attackValue;
        card.attackColor = attackColor;
        card.defenseValue = defenseValue;
        card.defenseColor = defenseColor;
        card.staminaCost = staminaCost;
        return cardObject;
    }

    public GameObject CreateCardObject(PersistentCard persistentCard)
    {
        GameObject cardObject = Instantiate(this.cardObject, Vector3.zero, Quaternion.identity);
        Card card = cardObject.GetComponent<Card>();
        card.attackValue = persistentCard.attackValue;
        card.attackColor = persistentCard.attackColor;
        card.defenseValue = persistentCard.defenseValue;
        card.defenseColor = persistentCard.defenseColor;
        card.staminaCost = persistentCard.staminaCost;
        return cardObject;

    }

    public GameObject GetCopy(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<Card>();
        return this.CreateCardObject(card.attackValue, card.attackColor, card.defenseValue, card.defenseColor, card.staminaCost);
    }
}
