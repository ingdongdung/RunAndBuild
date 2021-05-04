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
    public bool onceForCoroutine;
    public Coroutine SearchCoroutine;

    private void Awake()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();

        uiCanvas = GameObject.Find("UI Canvas for enemy").GetComponent<Canvas>();

        meetEnemy = false;
        onceForCoroutine = false;
        SearchCoroutine = null;
    }

    private void OnEnable()
    {
        meetEnemy = false;
        onceForCoroutine = false;
        SearchCoroutine = null;
    }

    private void OnDisable()
    {
        onceForCoroutine = false;
        meetEnemy = false;

        if (SearchCoroutine != null)
        {
            StopCoroutine(SearchCoroutine);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (meetEnemy && !onceForCoroutine)
        {
            SearchCoroutine = StartCoroutine(DistanceToEnemyTimer());
            onceForCoroutine = true;
        }
    }

    IEnumerator DistanceToEnemyTimer()
    {
        Search();
        yield return new WaitForSeconds(0.25f);
    }

    public int Search()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        int saveNumber = 0;

        if (enemyArray.Length != 0)
        {
            float minDistance = DistanceToEnemy(enemyArray[0]);
            for (int i = 1; i < enemyArray.Length; ++i)
            {
                if (minDistance >= DistanceToEnemy(enemyArray[i]))
                {
                    minDistance = DistanceToEnemy(enemyArray[i]);
                    saveNumber = i;
                }
            }

            return saveNumber;
        }
        else
        {
            print("그런건 없어용(Search()'s return value is -1)");
            return -1;
        }
    }

    public float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }
}
