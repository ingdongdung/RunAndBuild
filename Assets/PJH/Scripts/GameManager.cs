using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public AngelController ac;
    public DemonicController dc;
    public FairyController fc;
    public CharacterHpBarController achb;
    public CharacterHpBarController dchb;
    public CharacterHpBarController fchb;
    public Image aHpBarImage;
    public Image dHpBarImage;
    public Image fHpBarImage;
    public Button aBtn;
    public Button dBtn;
    public Button fBtn;
    public Canvas uiCanvas;
    public Text gameTimerText;
    public Text moneyText;
    public Slider gameProgressBar;

    public GameObject[] enemyArray;
    public bool meetEnemy;
    public bool onceForCoroutine;
    public Coroutine SearchCoroutine;

    public int gameLevel;
    public float gameTimerSec;
    public int gameTimerMin;
    public int gameMoney;

    public Vector3 bossDir;
    public Transform bossTra;
    public float rangedBossNormalAttackDamage;
    public float firstBossSkillAttackDamage;
    public float middleBossSkillAttackDamage;
    public float finalBossSkillAttackDamage;
    public Vector3 BossSkillDir;

    private Vector3 baseDirection;

    private void Awake()
    {
        fc = GameObject.Find("Fairy").GetComponent<FairyController>();
        ac = GameObject.Find("Angel").GetComponent<AngelController>();
        dc = GameObject.Find("Demonic").GetComponent<DemonicController>();

        fchb = GameObject.Find("FairyHpBar").GetComponent<CharacterHpBarController>();
        achb = GameObject.Find("AngelHpBar").GetComponent<CharacterHpBarController>();
        dchb = GameObject.Find("DemonicHpBar").GetComponent<CharacterHpBarController>();

        aBtn = GameObject.Find("AngelButton").GetComponent<Button>();
        dBtn = GameObject.Find("DemonicButton").GetComponent<Button>();
        fBtn = GameObject.Find("FairyButton").GetComponent<Button>();

        fHpBarImage = fchb.GetComponentsInChildren<Image>()[1];
        fHpBarImage.fillAmount = fc.MAXHP;
        aHpBarImage = achb.GetComponentsInChildren<Image>()[1];
        aHpBarImage.fillAmount = ac.MAXHP;
        dHpBarImage = dchb.GetComponentsInChildren<Image>()[1];
        dHpBarImage.fillAmount = dc.MAXHP;

        uiCanvas = GameObject.Find("UI Canvas for enemy").GetComponent<Canvas>();

        gameTimerText = GameObject.Find("GameTimerText").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();

        gameProgressBar = GameObject.Find("GameProgressBar").GetComponent<Slider>();

        meetEnemy = false;
        onceForCoroutine = false;
        SearchCoroutine = null;

        baseDirection = new Vector3(0f, 0f, 1f) - new Vector3(0f, 0f, 0f);
        baseDirection.Normalize();

        gameLevel = 2;

        gameTimerSec = 60f;
        gameTimerMin = 2;
        gameMoney = 0;

        bossDir = new Vector3(0f, 0f, 0f);
        bossTra = transform;
        rangedBossNormalAttackDamage = 40f;
        firstBossSkillAttackDamage = 50f;
        middleBossSkillAttackDamage = 65f;
        finalBossSkillAttackDamage = 25f;
        BossSkillDir = new Vector3(0f, 0f, 0f);

        switch (SceneManager.GetActiveScene().name)
        {
            case "firstStage":
                {
                    DataManager.Instance.UserData.currentScene = Scene.FirstStage;
                    break;
                }
            case "MiddleStage":
                {
                    DataManager.Instance.UserData.currentScene = Scene.MiddleStage;
                    break;
                }
            case "FinalStage":
                {
                    DataManager.Instance.UserData.currentScene = Scene.FinalStage;
                    break;
                }
        }
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
        gameLevel = 0;

        if (SearchCoroutine != null)
        {
            StopCoroutine(SearchCoroutine);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameProgressBar.value = gameLevel * 0.33f;
    }

    // Update is called once per frame
    void Update()
    {
        GameTimerUpdate();

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
            ++gameLevel;
            gameProgressBar.value = gameLevel * 0.33f;

            if (gameLevel == 4)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "firstStage":
                        {
                            DataManager.Instance.UserData.isFirstStage = true;
                            break;
                        }
                    case "MiddleStage":
                        {
                            DataManager.Instance.UserData.isMiddleStage = true;
                            break;
                        }
                    case "FinalStage":
                        {
                            DataManager.Instance.UserData.isFinalStage = true;
                            break;
                        }
                }
                DataManager.Instance.UserData.isClear = true;
                DataManager.Instance.SaveJsonData(DataManager.Instance.UserData);
                SceneLoadManager.Instance.MoveScene(Scene.StageEnd);
            }

            SpawnManager.Instance.MethodForStartingTreeSpawn();
            SpawnManager.Instance.MethodForStartingMonsterSpawn();
            SpawnManager.Instance.treeSpawnFlag = true;

            InitializeCharacter();
        }
    }

    private void GameTimerUpdate()
    {
        gameTimerSec -= Time.deltaTime;

        gameTimerText.text = string.Format("{0:D2}:{1:D2}", gameTimerMin, (int)gameTimerSec);

        if (gameTimerSec <= 0f)
        {
            gameTimerSec = 60f;
            --gameTimerMin;
        }
    }

    private void InitializeCharacter()
    {
        if (fc)
        {
            fc.Initialize();
            fc.transform.rotation = Quaternion.Euler(baseDirection);
            fc.animator.SetBool("Fly Forward", true);
        }

        if (ac)
        {
            ac.Initialize();
            ac.transform.rotation = Quaternion.Euler(baseDirection);
            ac.animator.SetBool("Run", true);
        }

        if (dc)
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
