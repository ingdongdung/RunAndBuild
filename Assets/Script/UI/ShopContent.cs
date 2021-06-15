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
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 1, "베이커리", 100, "설명설명설명설명1"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 2, "목공소", 200, "설명설명설명설명1"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 3, "풍력발전소", 300, "설명설명설명설명1"));

        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 4, "임시아이템1", 100, "설명설명설명설명2"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 5, "임시아이템2", 200, "설명설명설명설명2"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Interior, 6, "임시아이템3", 300, "설명설명설명설명2"));

        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 7, "임시아이템4", 100, "설명설명설명설명3"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 8, "임시아이템5", 200, "설명설명설명설명3"));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Environment, 9, "임시아이템6", 300, "설명설명설명설명3"));
    }
}
