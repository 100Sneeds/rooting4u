using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionIndexText : MonoBehaviour
{
    public HandSelectionMarker handSelectionMarker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text textMesh = GetComponent<TMP_Text>();
        textMesh.text = this.handSelectionMarker.slotNumber.ToString();
    }
}
