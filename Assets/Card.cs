using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int statDefence;
    public int statAttack;
    public int colourDefence;
    public int colourAttack;

    // Start is called before the first frame update
    void Start()
    {
        statDefence = Random.Range(0, 5);   // Init with random damage value
        statAttack = Random.Range(0, 5);    // Init with random damage value
        colourDefence = Random.Range(0, 3); // Init with random colour value
        colourAttack = Random.Range(0, 3);  // Init with random colour value
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play
    public void PlayCard()
    {

    }

    int CalculateDamageToDefender(Card attacker, Card defender)
    {
        if (attacker.colourAttack == defender.colourDefence)
        {
            return Mathf.Clamp(attacker.statAttack - defender.statDefence, 0, 10);
        }
        return attacker.statAttack;
    }
}
