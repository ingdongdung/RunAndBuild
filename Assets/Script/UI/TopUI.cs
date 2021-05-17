using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이름. 돈. 심장
public class TopUI : MonoBehaviour
{
    public void SetData(UserData data)
    {
        this.nameText.text = data.userName;
        this.moneyText.text = data.money.ToString();
        this.heartText.text = data.heart.ToString();
    }

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text heartText;
}
