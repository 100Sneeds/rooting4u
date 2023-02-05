using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
public class OpponentRandomizer : MonoBehaviour
{
    public SpriteLibraryAsset pink;
    public SpriteLibraryAsset green;
    public SpriteLibraryAsset orange;
    public SpriteLibraryAsset purple;
    public SpriteLibraryAsset yellow;

    public int numberOfOpponentSkins = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SpriteLibraryAsset GetRandomSpriteLibraryAsset()
    {
        int skinNumber = Random.Range(0, numberOfOpponentSkins);
        switch (skinNumber)
        {
            default:
            case 0:
                return pink;
            case 1:
                return green;
            case 2:
                return orange;
            case 3:
                return purple;
            case 4:
                return yellow;
        }
    }
}
