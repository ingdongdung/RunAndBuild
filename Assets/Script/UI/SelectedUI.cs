using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectedUI : MonoBehaviour
{
    private BuildingObjPool buildingObjPool;
    void Start()
    {
        buildingObjPool = FindObjectOfType<BuildingObjPool>();
    }
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

            this.BuildingBuy(this.data.Name);
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

    private void BuildingBuy(string buildingName)
    {
        // [TODO] 여기에 설치 행동 추가해주세요
 
        if(buildingName == "빵집")
        {
            buildingObjPool.CreateObject("House_2");
        }
        else if(buildingName == "대장간")
        {
            buildingObjPool.CreateObject("House_1");
        }
        else if(buildingName == "풍차")
        {
            buildingObjPool.CreateObject("Windmill");
        }
        else if (buildingName == "쿠키성")
        {
            buildingObjPool.CreateObject("Temple");
        }
        else if (buildingName == "시계탑")
        {
            buildingObjPool.CreateObject("TownHall");
        }
        else if (buildingName == "조각상")
        {
            buildingObjPool.CreateObject("Tower");
        }
        else if (buildingName == "뾰족 정원수")
        {
            buildingObjPool.CreateObject("Spruce_B");
        }
        else if (buildingName == "동그란 상록수")
        {
            buildingObjPool.CreateObject("Birch_B");
        }
        else if (buildingName == "회양목")
        {
            buildingObjPool.CreateObject("Bush_02");
        }
        else if (buildingName == "바위")
        {
            buildingObjPool.CreateObject("Mesh1");
        }

    }
}
