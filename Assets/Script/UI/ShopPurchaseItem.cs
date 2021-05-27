using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopPurchaseItem : MonoBehaviour
{
    public void SetItem(PurchaseItemData data, Action<bool> isBuy)
    {
        this.isBuy = isBuy;
        this.data = data;
        this.itemName.text = data.Name;
        this.price.text = data.Price.ToString();
        // this.image.sprite = sprite;
        // this.image.sprite = Instantiate(Resources.Load("ShopImg/shop_" + data.ID.ToString()), Vector3.zero, Quaternion.identity) as Sprite;
        // this.image.sprite = ImageManager.Instance.GetShopImage(data.ID);
    }

    public void OnClickBuy()
    {
        this.isBuy(true);
    }

    [SerializeField] private Text itemName;
    [SerializeField] private Text price;
    [SerializeField] private Image image;

    private PurchaseItemData data;
    private Action<bool> isBuy;
}
