using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseItem : MonoBehaviour
{
    public void SetItem(PurchaseItemData data, Sprite sprite)
    {
        this.data = data;
        this.itemName.text = data.Name;
        this.price.text = data.Price.ToString();
        this.image.sprite = sprite;
        // this.image.sprite = Instantiate(Resources.Load("ShopImg/shop_" + data.id.ToString()), Vector3.zero, Quaternion.identity) as Sprite;
        // this.image.sprite = ImageManager.Instance.GetShopImage(data.id);
    }

    public void OnClickBuy()
    {
        if (DataManager.Instance.UseCoin(data.Price))
        {
            Debug.Log(this.data.Name);
            DataManager.Instance.PurchaseItem(this.data.Name);
            KingdomScene.Instance.Refresh();
        }
    }

    [SerializeField] private Text itemName;
    [SerializeField] private Text price;
    [SerializeField] private Image image;

    private PurchaseItemData data;
}
