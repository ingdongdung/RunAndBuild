using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ImageManager : SingleTonObject<ImageManager>
{
    // public Sprite GetShopImage(int id)
    // {
    //     Debug.Log(this.shopAtlas);
    //     return this.shopAtlas.GetSprite("shop_" + id);
    // }

    [SerializeField] private SpriteAtlas shopAtlas;
}
