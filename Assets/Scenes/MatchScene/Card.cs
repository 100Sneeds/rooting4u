using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public enum BoostedState
    {
        NoBoost,
        PerformancePenalized,
        PerformanceBoosted,
    }

    public int attackValue;
    public CardColor attackColor;
    public int defenseValue;
    public CardColor defenseColor;

    public int staminaCost;

    public TMP_Text attackValueText;
    public TMP_Text defenseValueText;
    public TMP_Text staminaCostText;

    public BoostedState boostedState = BoostedState.NoBoost;

    private static Color32 DEFAULT_OUTLINE_COLOR = Color.black;
    private static Color32 BOOSTED_OUTLINE_COLOR = Color.green;
    private static Color32 PENALIZED_OUTLINE_COLOR = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        staminaCostText.text = this.staminaCost.ToString();
        this.ShowAttackColor(this.attackColor);
        this.ShowDefenseColor(this.defenseColor);
    }

    // Update is called once per frame
    void Update()
    {
        attackValueText.text = this.GetModifiedAttackValue().ToString();
        defenseValueText.text = this.GetModifiedDefenseValue().ToString();
        if (this.boostedState == BoostedState.NoBoost)
        {
            attackValueText.outlineColor = DEFAULT_OUTLINE_COLOR;
            defenseValueText.outlineColor = DEFAULT_OUTLINE_COLOR;
        }
        if (this.boostedState == BoostedState.PerformanceBoosted)
        {
            attackValueText.outlineColor = BOOSTED_OUTLINE_COLOR;
            defenseValueText.outlineColor = BOOSTED_OUTLINE_COLOR;
        }
        if (this.boostedState == BoostedState.PerformancePenalized)
        {
            attackValueText.outlineColor = PENALIZED_OUTLINE_COLOR;
            defenseValueText.outlineColor = PENALIZED_OUTLINE_COLOR;
        }
        attackValueText.outlineWidth = 0.2f;
        defenseValueText.outlineWidth = 0.2f;
    }

    public int GetModifiedAttackValue()
    {
        if (boostedState == BoostedState.PerformancePenalized)
        {
            return this.attackValue - 1;
        }
        if (boostedState == BoostedState.PerformanceBoosted)
        {
            return this.attackValue + 1;
        }
        return this.attackValue;
    }

    public int GetModifiedDefenseValue()
    {
        if (boostedState == BoostedState.PerformancePenalized)
        {
            return this.defenseValue - 1;
        }
        if (boostedState == BoostedState.PerformanceBoosted)
        {
            return this.defenseValue + 1;
        }
        return this.defenseValue;
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
        this.HideChildSprites("AttackHalf");
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
        this.HideChildSprites("DefenseHalf");
        GameObject defenseObject = this.transform.Find("DefenseHalf").Find(defenseObjectName).gameObject;
        SpriteRenderer spriteRenderer = defenseObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    public void ShowAll()
    {
        this.ShowAttack();
        this.ShowDefense();
        this.ShowStamina();
    }

    public void ShowAttack()
    {
        this.ShowChild("AttackHalf");
        this.ShowAttackColor(this.attackColor);
    }

    public void ShowDefense()
    {
        this.ShowChild("DefenseHalf");
        this.ShowDefenseColor(this.defenseColor);
    }

    public void ShowStamina()
    {
        this.ShowChild("Stamina");
    }

    public void ShowChild(string childName)
    {
        GameObject child = this.transform.Find(childName).gameObject;
        SpriteRenderer[] spriteRenderers = child.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
        }

        TMP_Text[] texts = child.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            Color color = text.color;
            color.a = 1;
            text.color = color;
        }
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

    private void HideChildSprites(string childName)
    {
        GameObject child = this.transform.Find(childName).gameObject;
        SpriteRenderer[] spriteRenderers = child.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }
    }
}
