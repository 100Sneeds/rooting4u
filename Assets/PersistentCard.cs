using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCard
{
    public int attackValue;
    public CardColor attackColor;
    public int defenseValue;
    public CardColor defenseColor;
    public int staminaCost;

    public PersistentCard(int attackValue, CardColor attackColor, int defenseValue, CardColor defenseColor, int staminaCost)
    {
        this.attackValue = attackValue;
        this.attackColor = attackColor;
        this.defenseValue = defenseValue;
        this.defenseColor = defenseColor;
        this.staminaCost = staminaCost;
    }
}
