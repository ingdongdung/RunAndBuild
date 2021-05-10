using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public FairyController fc;
    public AngelController ac;
    public DemonicController dc;
    public CharacterHpBarController fchb;
    public CharacterHpBarController achb;
    public CharacterHpBarController dchb;
    public Image fHpBarImage;
    public Image aHpBarImage;
    public Image dHpBarImage;
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

        fchb = GameObject.Find("FairyHpBar").GetComponent<CharacterHpBarController>();
        achb = GameObject.Find("AngelHpBar").GetComponent<CharacterHpBarController>();
        dchb = GameObject.Find("DemonicHpBar").GetComponent<CharacterHpBarController>();

        fHpBarImage = fchb.GetComponentsInChildren<Image>()[1];
        fHpBarImage.fillAmount = fc.MAXHP;
        aHpBarImage = achb.GetComponentsInChildren<Image>()[1];
        aHpBarImage.fillAmount = ac.MAXHP;
        dHpBarImage = dchb.GetComponentsInChildren<Image>()[1];
        dHpBarImage.fillAmount = dc.MAXHP;

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

        // �� ���� ���� �� �׾��� ��
        if (enemyArray.Length == 0 && meetEnemy && onceForCoroutine)
        {
            StopCoroutine(SearchCoroutine);
            meetEnemy = false;
            onceForCoroutine = false;

            SpawnManager.Instance.treeSpawningCoroutine = StartCoroutine(SpawnManager.Instance.StartTreeSpawning());
            SpawnManager.Instance.monsterSpawningCoroutine = StartCoroutine(SpawnManager.Instance.StartMonsterSpawning());
            SpawnManager.Instance.treeSpawnFlag = true;

            InitializeCharacter();
        }
    }

    private void InitializeCharacter()
    {
        if (fc.enabled)
        {
            fc.Initialize();
            fc.transform.rotation = Quaternion.Euler(baseDirection);
            fc.animator.SetBool("Fly Forward", true);
        }

        if (ac.enabled)
        {
            ac.Initialize();
            ac.transform.rotation = Quaternion.Euler(baseDirection);
            ac.animator.SetBool("Run", true);
        }

        if (dc.enabled)
        {
            dc.Initialize();
            dc.transform.rotation = Quaternion.Euler(baseDirection);
            dc.animator.SetBool("Run", true);
        }
    }

    IEnumerator DistanceToEnemyTimer()
    {
        SearchEnemy();
        yield return new WaitForSeconds(0.25f);
    }

    public int SearchEnemy()
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
            Debug.Log("Search()'s return value is -1");
            return -1;
        }
    }

    public float DistanceToEnemy(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position);
    }
}
