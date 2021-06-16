using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
            this.isClose(true);

            this.BuildingAction();
        }
    }
    public void OnClickBuilding()
    {
        KingdomScene.Instance.Refresh();
        this.isClose(true);
        
        this.BuildingAction();
    }

    public void Init(PurchaseItemData item, Sprite sprite, Action<bool> isClose)
    {
        this.data = item;
        this.titleText.text = item.Name;
        this.descText.text = item.Desc;
        this.priceText.text = item.Price.ToString();
        this.image.sprite = sprite;
        this.isClose = isClose;

        bool isPurchased = DataManager.Instance.UserData.itemList.Find(l => l.name == this.data.Name) != null;
        this.buyBtn.SetActive(!isPurchased);
        this.purchasedBtn.SetActive(isPurchased);

        // this.image.sprite = ImageManager.Instance.GetShopImage(item.ID);
    }

    [SerializeField] private Text titleText;
    [SerializeField] private Text descText;
    [SerializeField] private Text priceText;
    [SerializeField] private Image image;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private GameObject purchasedBtn;
    private PurchaseItemData data;
    private Action<bool> isClose;

    private void UpdateData()
    {

    }

    private void BuildingAction()
    {
        // [TODO] 여기에 설치 행동 추가해주세요
    }
}
