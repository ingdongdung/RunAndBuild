using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum ShopCategory
{
    BUILDING = 0,
    Interior,
    Environment,
}

public class ShopContent : MonoBehaviour
{
    public void OnClickCategory(int cat)
    {
        this.tab = (ShopCategory)cat;
        this.Init();
    }

    public Sprite GetShopImage(int id)
    {
        return this.atlas.GetSprite("shop_" + id);
    }

    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    protected void OnEnable()
    {
        this.LoadData();
        this.tab = ShopCategory.BUILDING;
        this.Init();
    }

    // Update is called once per frame
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private ShopPurchaseItem shopItemPrefab;
    [SerializeField] private SelectedUI selectedUI;
    [SerializeField] private GameObject content;
    [SerializeField] private List<ShopCategoryItem> categories;
    [SerializeField] private List<PurchaseItemData> itemDataList = new List<PurchaseItemData>();
    private ShopCategory tab;

    private void Init()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.GetChild(i));
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (ShopCategoryItem item in categories)
        {
            item.SeleteItem(this.tab);
        }
        this.SetItem(this.tab);
    }

    private void SetItem(ShopCategory category)
    {
        List<PurchaseItemData> list = this.itemDataList.FindAll(l => l.Category == category);
        if (list != null && list.Count != 0)
        {
            foreach (PurchaseItemData data in list)
            {
                ShopPurchaseItem item = Instantiate<ShopPurchaseItem>(this.shopItemPrefab, this.content.transform);
                item.SetItem(data, this.GetShopImage(data.ID), (isBuy) =>
                {
                    this.selectedUI.Init(data, this.GetShopImage(data.ID), (isClose) =>
                    {
                        this.selectedUI.gameObject.SetActive(false);
                        this.gameObject.SetActive(false);
                    });
                    this.selectedUI.gameObject.SetActive(true);
                });
            }
        }
    }

    private void LoadData()
    {
        this.itemDataList.Clear();
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 1, "빵집", 100, "빵 냄새를 맡고 몰려든 요정으로 북적북적! 보기만 해도 배가 고파지는 빵 모양 지붕 덕분에 멀리서 찾아오는 손님도 있을 정도다.")); 
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 2, "대장간", 100, "대장간에서 나오는 연기는 대장장이의 땅과 노력! 왕국 안팎을 다니며 고군분투하는 요정덕에 대장간은 연기가 멈출 날 없다."));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 3, "풍차", 100, "바람을 이용해 비스킷을 만드는 시설. 가루가 날리기 때문에 일할 땐 재채기도 조심해야 한다. 재채기 유발 요정은 접근 금지!"));

        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 4, "쿠키성", 500, "킹덤의 용감한 요정 일행이 세운 왕국 중앙에 있는 성으로 왕국의 성장척도를 나타내는 건물"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 5, "시계탑", 200, "높이 솟은 시계탑은 만남의 장소로도 안성맞춤! 언제나 정확하기 때문에 손목시계를 장만할 필요가 없다."));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 6, "조각상", 300, "오래 전 요정들의 영웅을 따랐던 충실한 젤리라이언을 묘사한 조각상. 옛 친구를 그리워하던 마음은 아직까지도 눈물이 되어 흐르고 있다."));

        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 7, "동그란 상록수", 10, "싱그러운 이파리를 동그란 모양으로 다듬었다. 한쪽에만 자라난 덤불은 소소한 매력 포인트."));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 8, "뾰족 정원수", 10, "꼭대기에 별을 달기 딱 좋아보이는 튼튼하고 예쁜 초록빛 나무. 가끔 어린 요정들이 날린 풍선이 걸리기도 한다."));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 9, "회양목", 20, "동그란 정원수."));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 10, "바위", 15, "울퉁불퉁 바위")); 
    }
}
