using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public struct OpponentSkinData
{
    private SpriteLibraryAsset spriteLibraryAsset;
    private Color color;

    public OpponentSkinData(SpriteLibraryAsset spriteLibraryAsset, Color color)
    {
        this.spriteLibraryAsset = spriteLibraryAsset;
        this.color = color;
    }

    public SpriteLibraryAsset GetSpriteLibraryAsset()
    {
        return this.spriteLibraryAsset;
    }

    public Color GetColor()
    {
        return this.color;
    }
}
