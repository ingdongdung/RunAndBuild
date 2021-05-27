using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUI : MonoBehaviour
{
    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickBuy()
    {
        if (DataManager.Instance.UseCoin(this.data.Price))
        {
            Debug.Log(this.data.Name);
            DataManager.Instance.PurchaseItem(this.data.Name);
            KingdomScene.Instance.Refresh();
        }
    }

    public void Init(PurchaseItemData item)
    {
        this.data = item;
        this.titleText.text = item.Name;
        this.descText.text = item.Desc;
        this.priceText.text = item.Price.ToString();
        // this.image.sprite = ImageManager.Instance.GetShopImage(item.ID);
    }

    [SerializeField] private Text titleText;
    [SerializeField] private Text descText;
    [SerializeField] private Text priceText;
    [SerializeField] private Image image;
    private PurchaseItemData data;

    private void UpdateData()
    {

    }
}
