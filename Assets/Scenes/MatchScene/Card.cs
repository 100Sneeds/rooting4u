using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int attackValue;
    public CardColor attackColor;
    public int defenseValue;
    public CardColor defenseColor;

    public int staminaCost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), this.attackValue.ToString());
        GUI.Label(new Rect(0, 3, 100, 100), this.defenseValue.ToString());
        GUI.Label(new Rect(0, 6, 100, 100), this.staminaCost.ToString());
    }

    public int CalculateDamageToDefender(Card attacker, Card defender)
    {
        if (attacker.attackColor == defender.defenseColor)
        {
            int reducedAttack = attacker.attackValue - defender.defenseValue;
            if (reducedAttack < 0)
            {
                return 0;
            }
            else
            {
                return reducedAttack;
            }
        }
        return attacker.attackValue;
    }
}
