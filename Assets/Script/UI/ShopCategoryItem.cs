using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCategoryItem : MonoBehaviour
{
    public void SeleteItem(ShopCategory category)
    {
        SelectImg.SetActive(category == this.category);
        baseImg.SetActive(category != this.category);
    }

    [SerializeField] private GameObject SelectImg;
    [SerializeField] private GameObject baseImg;
    [SerializeField] private ShopCategory category;
}
