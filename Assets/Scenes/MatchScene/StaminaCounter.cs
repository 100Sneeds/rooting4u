using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaminaCounter : MonoBehaviour
{
    public MatchPlayer matchPlayer;
    public TMP_Text staminaText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.staminaText.text = matchPlayer.currentStamina.ToString();
    }
}
