using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone : MonoBehaviour
{

    public int player;
    public Card[] cards;


    public CardZone(int _player){
        cards = new Card[3];    // init array with 3 random cards
        player = _player;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
