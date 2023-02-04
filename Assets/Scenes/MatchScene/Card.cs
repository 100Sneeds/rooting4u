using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public int attackValue;
    public CardColor attackColor;
    public int defenseValue;
    public CardColor defenseColor;

    public int staminaCost;

    public TMP_Text attackValueText;
    public TMP_Text defenseValueText;
    public TMP_Text staminaCostText;

    // Start is called before the first frame update
    void Start()
    {
        attackValueText.text = this.attackValue.ToString();
        defenseValueText.text = this.defenseValue.ToString();
        staminaCostText.text = this.staminaCost.ToString();
        this.ShowAttackColor(this.attackColor);
        this.ShowDefenseColor(this.defenseColor);
    }

    // Update is called once per frame
    void Update()
    {

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

    public void ShowAttackColor(CardColor cardColor)
    {
        string attackObjectName;
        switch (cardColor)
        {
            default:
            case CardColor.Red:
                attackObjectName = "AttackRed";
                break;
            case CardColor.Green:
                attackObjectName = "AttackGreen";
                break;
            case CardColor.Blue:
                attackObjectName = "AttackBlue";
                break;
        }
        GameObject attackObject = this.transform.Find("AttackHalf").Find(attackObjectName).gameObject;
        SpriteRenderer spriteRenderer = attackObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    public void ShowDefenseColor(CardColor cardColor)
    {
        string defenseObjectName;
        switch (cardColor)
        {
            default:
            case CardColor.Red:
                defenseObjectName = "DefenseRed";
                break;
            case CardColor.Green:
                defenseObjectName = "DefenseGreen";
                break;
            case CardColor.Blue:
                defenseObjectName = "DefenseBlue";
                break;
        }
        GameObject defenseObject = this.transform.Find("DefenseHalf").Find(defenseObjectName).gameObject;
        SpriteRenderer spriteRenderer = defenseObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }


    public void HideAll()
    {
        this.HideAttack();
        this.HideDefense();
        this.HideStamina();
    }

    public void HideAttack()
    {
        this.HideChild("AttackHalf");
    }

    public void HideDefense()
    {
        this.HideChild("DefenseHalf");
    }

    public void HideStamina()
    {
        this.HideChild("Stamina");
    }

    public void HideChild(string childName)
    {
        GameObject child = this.transform.Find(childName).gameObject;
        SpriteRenderer[] spriteRenderers = child.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }

        TMP_Text[] texts = child.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            Color color = text.color;
            color.a = 0;
            text.color = color;
        }
    }
}
