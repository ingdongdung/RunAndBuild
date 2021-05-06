using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public void OnClickBackground()
    {
        Debug.Log("Move KingdomScene");
        SceneLoadManager.Instance.MoveScene(Scene.KingdomScene);
    }
}
