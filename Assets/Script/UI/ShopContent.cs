using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopCategory
{
    BUILDING = 0,
    AA,
    BB,
}

public class ShopContent : MonoBehaviour
{
    public void OnClickCategory(int cat)
    {
        this.tab = (ShopCategory)cat;
        this.Init();
    }
    // Start is called before the first frame update
    protected void Start()
    {
        this.LoadData();
        this.tab = ShopCategory.BUILDING;
        this.Init();
    }

    // Update is called once per frame

    [SerializeField] private ShopPurchaseItem shopItemPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private List<ShopCategoryItem> categories;
    [SerializeField] private List<PurchaseItemData> itemDataList = new List<PurchaseItemData>();
    private ShopCategory tab;

    private void Init()
    {
        foreach (ShopCategoryItem item in categories)
        {
            item.SeleteItem(this.tab);
            SetItem(this.tab);
        }
    }

    private void SetItem(ShopCategory cat)
    {
        List<PurchaseItemData> list = this.itemDataList.FindAll(l => l.category == cat);
        foreach (PurchaseItemData data in list)
        {
            ShopPurchaseItem item = Instantiate<ShopPurchaseItem>(this.shopItemPrefab, this.content.transform);
            item.SetItem(data);
        }
    }

    private void LoadData()
    {
        itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 1, "빵야빵야", 100));
        itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 2, "퍽 퍽", 200));
        itemDataList.Add(new PurchaseItemData(ShopCategory.BUILDING, 3, "휘리릭", 300));
    }
}
