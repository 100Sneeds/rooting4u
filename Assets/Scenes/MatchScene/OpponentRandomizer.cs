using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class OpponentRandomizer : MonoBehaviour
{
    public SpriteLibraryAsset blue;
    public SpriteLibraryAsset pink;
    public SpriteLibraryAsset green;
    public SpriteLibraryAsset orange;
    public SpriteLibraryAsset purple;
    public SpriteLibraryAsset yellow;

    public int numberOfOpponentSkins = 5;

    

    public static Color BLUE = new Color(58 /255f, 184 /255f, 203 /255f);
    public static Color PINK = new Color(203 /255f, 58 /255f, 140 /255f);
    public static Color GREEN = new Color(96 /255f, 203 /255f, 58 /255f);
    public static Color YELLOW = new Color(238 /255f, 186 /255f, 73 /255f);
    public static Color ORANGE = new Color(238 /255f, 109 /255f, 70 /255f);
    public static Color PURPLE = new Color(154 /255f, 58 /255f, 214 /255f);

    private Dictionary<OpponentSkin, OpponentSkinData> skinData = new Dictionary<OpponentSkin, OpponentSkinData>();

    // Start is called before the first frame update
    void Start()
    {
        skinData.Add(OpponentSkin.Blue, new OpponentSkinData(blue, BLUE));
        skinData.Add(OpponentSkin.Pink, new OpponentSkinData(pink, PINK));
        skinData.Add(OpponentSkin.Green, new OpponentSkinData(green, GREEN));
        skinData.Add(OpponentSkin.Yellow, new OpponentSkinData(yellow, YELLOW));
        skinData.Add(OpponentSkin.Orange, new OpponentSkinData(orange, ORANGE));
        skinData.Add(OpponentSkin.Purple, new OpponentSkinData(purple, PURPLE));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public OpponentSkinData GetSkinData(OpponentSkin opponentSkin)
    {
        return this.skinData.GetValueOrDefault(opponentSkin);
    }

    public OpponentSkin GetRandomOpponentSkin()
    {
        int skinNumber = Random.Range(0, numberOfOpponentSkins);
        switch (skinNumber)
        {
            default:
            case 0:
                return OpponentSkin.Pink;
            case 1:
                return OpponentSkin.Green;
            case 2:
                return OpponentSkin.Orange;
            case 3:
                return OpponentSkin.Purple;
            case 4:
                return OpponentSkin.Yellow;
        }
    }
}
