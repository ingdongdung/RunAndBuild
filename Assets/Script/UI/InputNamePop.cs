using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNamePop : MonoBehaviour
{
    public void EndEdit()
    {
        Debug.Log(inputText.text);

        string strFile = string.Format(StringConst.ID_PATH, inputText.text);
        FileInfo fileInfo = new FileInfo(strFile);
        if (fileInfo.Exists)
        {
            DataManager.Instance.LoadJsonData(inputText.text);
        }
        else
        {
            DataManager.Instance.SaveJsonData(new UserData(inputText.text, 1000, 5));
        }

        SceneLoadManager.Instance.MoveScene(Scene.KingdomScene);
    }

    [SerializeField]
    private Text inputText;
}
