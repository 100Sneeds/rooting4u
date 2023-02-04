using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandSelectionMarker : MonoBehaviour
{
    public int slotNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }

        TMP_Text[] texts = this.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            Color color = text.color;
            color.a = 0;
            text.color = color;
        }
    }

    public void Show()
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
        }

        TMP_Text[] texts = this.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            Color color = text.color;
            color.a = 1;
            text.color = color;
        }
    }
}
