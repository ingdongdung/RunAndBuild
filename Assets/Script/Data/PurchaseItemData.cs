using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemData
{
    public ShopCategory Category => this.category;
    public string Name => this.name;
    public int Price => this.price;
    public PurchaseItemData(ShopCategory category, int id, string name, int price){
        this.category = category;
        this.id = id;
        this.price = price;
        this.name = name;
        this.category = category;
    }
    private int id;
    private string name;
    private int price;
    private ShopCategory category;
}
