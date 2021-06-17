using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    TitleScene,
    KingdomScene,
    FirstStage,
    MiddleStage,
    FinalStage,
    StageEnd,
}

public class SceneLoadManager : SingleTonObject<SceneLoadManager>
{
    public void MoveScene(Scene scene)
    {
        Debug.Log("다른 씬으로 이동합니다.");
        SceneManager.LoadScene((int)scene);
    }
}
