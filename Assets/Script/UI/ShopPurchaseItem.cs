using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseItem : MonoBehaviour
{
    public void SetItem(PurchaseItemData data)
    {
        this.itemName.text = data.name;
        this.price.text = data.price.ToString();
        // this.image.sprite = ImageManager.Instance.GetShopImage(data.id);
    }

    [SerializeField] private Text itemName;
    [SerializeField] private Text price;
    [SerializeField] private Image image;
}
