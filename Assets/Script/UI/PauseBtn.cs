using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    public void OnClickPause()
    {
        Time.timeScale = 0;
        this.pausePage.SetActive(true);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        this.pausePage.SetActive(false);
        SceneLoadManager.Instance.MoveScene(Scene.KingdomScene);
    }

    public void OnClickContinue()
    {
        Time.timeScale = 1;
        this.pausePage.SetActive(false);
    }

    public GameObject pausePage;
}
