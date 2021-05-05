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

    private Vector3 baseDirection;

    private void Awake()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();

        uiCanvas = GameObject.Find("UI Canvas for enemy").GetComponent<Canvas>();

        meetEnemy = false;
        onceForCoroutine = false;
        SearchCoroutine = null;

        baseDirection = new Vector3(0f, 0f, 1f) - new Vector3(0f, 0f, 0f);
        baseDirection.Normalize();
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

        // 눈 앞의 적이 다 죽었을 때
        if (enemyArray.Length == 0 && meetEnemy && onceForCoroutine)
        {
            StopCoroutine(SearchCoroutine);
            meetEnemy = false;
            onceForCoroutine = false;

            SpawnManager.Instance.treeSpawningCoroutine = StartCoroutine(SpawnManager.Instance.StartTreeSpawning());
            SpawnManager.Instance.treeSpawnFlag = true;

            fc.Initialize();
            ac.Initialize();
            dc.Initialize();


            fc.transform.rotation = Quaternion.Euler(baseDirection);
            ac.transform.rotation = Quaternion.Euler(baseDirection);
            dc.transform.rotation = Quaternion.Euler(baseDirection);


            fc.animator.SetBool("Fly Forward", true);
            ac.animator.SetBool("Run", true);
            dc.animator.SetBool("Run", true);
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

        if (enemyArray.Length > 0)
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
            print(enemyArray.Length);
            print("그런 건 없어용 (Search()'s return value is -1)");
            return -1;
        }
    }

    public float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }
}
