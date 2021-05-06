using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemData
{
    public PurchaseItemData(ShopCategory category, int id, string name, int price){
        this.category = category;
        this.id = id;
        this.price = price;
        this.name = name;
        this.category = category;
    }
    public int id;
    public string name;
    public int price;
    public ShopCategory category;
}
