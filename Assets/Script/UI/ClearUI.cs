using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Clear UI");
        bool isClear = true;
        failObj.SetActive(!isClear);
        clearObj.SetActive(isClear);
        foreach (CharacterUI charUI in characterList)
        {
            if (isClear)
            {
                charUI.ActionClear();
            }
            else
            {
                charUI.ActionFail();
            }
        }
    }

    [SerializeField] Text stageText;
    [SerializeField] GameObject clearObj;
    [SerializeField] GameObject failObj;
    [SerializeField] GameObject rewardUI;
    [SerializeField] List<CharacterUI> characterList;
}
