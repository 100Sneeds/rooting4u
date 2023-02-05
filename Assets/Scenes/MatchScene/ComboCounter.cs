using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{   
    public TMP_Text comboText;
    public int combo;
    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
    }

    public void Update(){
        if (comboText != null){
            comboText.text = combo.ToString();
        }
    }

    public void incrementCombo(){
        combo++;
    }

    public void resetCombo(){
        combo = 0;
    }
}
