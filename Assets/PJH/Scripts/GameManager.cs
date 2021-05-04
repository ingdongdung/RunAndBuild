using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public FairyController fc;
    public AngelController ac;
    public DemonicController dc;
    public Canvas uiCanvas;

    public GameObject[] enemyArray;
    public bool meetEnemy;

    private void Awake()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();

        uiCanvas = GameObject.Find("UI Canvas for enemy").GetComponent<Canvas>();

        meetEnemy = false;
    }

    private void OnEnable()
    {
        meetEnemy = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public float DistanceToEnemy()
    //{
    //    enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
    //    int saveNumber = 0;
    //    float minDistance = DistanceToEnemy(enemyArray[0]);
    //    for (int i = 1; i < enemyArray.Length; ++i)
    //    {
    //        //print("enemy" + i + " : " + DistanceToEnemy(enemyArray[i]));
    //        if (minDistance >= DistanceToEnemy(enemyArray[i]))
    //        {
    //            minDistance = DistanceToEnemy(enemyArray[i]);
    //            saveNumber = i;
    //        }
    //    }

    //    return Vector3.Distance(transform.position, enemy.transform.position);
    //}
}
