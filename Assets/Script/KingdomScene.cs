using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomScene : MonoBehaviour
{
    public void OnClickInGameScene()
    {
        SceneLoadManager.Instance.MoveScene(Scene.InGame);
    }

    public void OnClickShop()
    {
        shopContent.gameObject.SetActive(true);
    }

    [SerializeField] ShopContent shopContent;
}
