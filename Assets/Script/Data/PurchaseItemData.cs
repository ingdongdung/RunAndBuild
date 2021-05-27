using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemData
{
    public ShopCategory Category => this.category;
    public string Name => this.name;
    public string Desc => this.desc;
    public int Price => this.price;
    public int ID => this.id;
    public PurchaseItemData(ShopCategory category, int id, string name, int price, string desc)
    {
        this.category = category;
        this.id = id;
        this.price = price;
        this.name = name;
        this.desc = desc;
        this.category = category;
    }
    private int id;
    private string name;
    private string desc;
    private int price;
    private ShopCategory category;
}
