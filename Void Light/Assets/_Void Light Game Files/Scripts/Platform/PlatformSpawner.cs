using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlatformTypes))]
public class PlatformSpawner : MonoBehaviour {

    public static PlatformSpawner Instance;
    [Header("Prefabs")]
    public GameObject platformPrefab;
    public GameObject leaveAreaPrefab;

    [Header("Init Data")]
    public float leaveAreaOffset = 4f;
    public float platformFadeOffsetBy = 2f;
    private float platformFadeOffset;
    public float shiftSetAmountBy;
    private float shiftSetAmount;
    public bool printDebug = false;

    [Header("Amount Of Platform Per Set:")]
    public int maxPlatformPerSet;
    public int minPlatformPerSet;
    private int amountOfPlatformPerSet;

    [Header("Distance Between Platform:")]
    public float maxDistanceY;
    //This should not really be negetive maby fix later
    public float minDistanceY;
    public float maxDistanceX;
    public float minDistanceX;
    [Tooltip("Determines the scale of the set")]
    public float platOffSetAmountBy;
    private float platOffSetAmount;

    [Header("Spawn Wisps Setting")]
    public GameObject wispPrefab;
    public float spawnChance;
    public int maxWispPerSet = 1;
    private bool shouldSpawnWisp;
    private int wispSpawned;

    private GameObject leaveAreaGO;
    private GameObject[] platformInstances;
    private RandomPlatformTypes randomPlatformTypes;
    private int platformsSpawnedInRound;

    public int AmountOfPlatformPerSet
    {
        get
        {
            return amountOfPlatformPerSet;
        }

        set
        {
            amountOfPlatformPerSet = value;
        }
    }

    public GameObject[] PlatformInstances
    {
        get
        {
            return platformInstances;
        }
    }

    void Awake()
    {
        Instance = this;
        randomPlatformTypes = GetComponent<RandomPlatformTypes>();
        InitData();
        PlayerController.PlayerDead += GameManager_PlayerDead;
        GameManager.LeftArea += GameManager_LeftArea;
        PlayerController.PlayerReachedStartPlat += GameManager_PlayerReachedStartPlat;
        GameManager.GameSavePlayerCurrentStreak += GameManager_GameSavePlayerCurrentStreak;
        GameManager.GameStart += GameManager_GameStart;
    }

    private void GameManager_GameStart()
    {
        platformsSpawnedInRound = ScoreManager.Score;
    }

    private void GameManager_GameSavePlayerCurrentStreak()
    {
        platformsSpawnedInRound = 0;
        CleanUpPlatformInstances();
    }

    private void GameManager_PlayerReachedStartPlat()
    {
        InitData();
        CreatePlatforms();
    }

    private void GameManager_LeftArea()
    {
        ShiftSetUp();
        CleanUpPlatformInstances();
    }

    private void GameManager_PlayerDead()
    {
        platformsSpawnedInRound = 0;
        CleanUpPlatformInstances();
        ResetSelf();
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDead -= GameManager_PlayerDead;
        GameManager.LeftArea -= GameManager_LeftArea;
        PlayerController.PlayerReachedStartPlat -= GameManager_PlayerReachedStartPlat;
        GameManager.GameSavePlayerCurrentStreak -= GameManager_GameSavePlayerCurrentStreak;
        GameManager.GameStart -= GameManager_GameStart;
    }

    void InitData()
    {
        wispSpawned = 0;
        platformFadeOffset = 0;
        platOffSetAmount = 0;
        shouldSpawnWisp = ShouldSpawnWisp();
        AmountOfPlatformPerSet = Random.Range(minPlatformPerSet, maxPlatformPerSet);
        platformInstances = new GameObject[AmountOfPlatformPerSet];
    }

    public void CreatePlatforms()
    {
        //Make it return Data Set;
        PlatformType[] platformTypeSet = randomPlatformTypes.CreateSet();

        GameObject platformToSpawnWisp = null;
        int spawnSpot = Random.Range(0, AmountOfPlatformPerSet);

        for (int i = 0; i < AmountOfPlatformPerSet; i++)
        {
            float x = Random.Range(minDistanceX, maxDistanceX);
            float y = Random.Range(minDistanceY, maxDistanceY) + platOffSetAmount + shiftSetAmount;

            Vector2 calPlatPos = new Vector2(x, y);
            GameObject platformGO = Instantiate(platformPrefab, calPlatPos, Quaternion.identity) as GameObject;
            platformGO.transform.position = calPlatPos;
            Platform platform = platformGO.GetComponent<Platform>();

            platform.Id = i;
            platform.PlatformType = platformTypeSet[i];
            platformGO.name += "_" + i + "_" + name;
            PlatformInstances[i] = platformGO;
            platformGO.transform.parent = this.transform;
            platOffSetAmount += platOffSetAmountBy;

            platformsSpawnedInRound++;

            #region Handles showing highstreak on platform
            if (platformsSpawnedInRound == ScoreManager.HighScore)
            {
                platform.GetComponent<PlatformUIController>().CreateHighStreakUI();
            }
            #endregion

            if (spawnSpot == i)
            {
                platformToSpawnWisp = platformGO;
                //platform.ChangeRuneSprite(GlobelSprites.Instance.wispRuneSprite);
            }

            //FadeOut Platform
            platformFadeOffset += platformFadeOffsetBy;
            platform.fadePlatform.StartFadeOut(platformFadeOffset);

            //After Last platform create leave area part
            if (i == AmountOfPlatformPerSet - 1)
            {
                float spawnY = platformGO.transform.position.y + leaveAreaOffset;
                Vector2 spawnPos = new Vector2(0, spawnY);

                CreateLeaveAreaGO(spawnPos);
            }
        }

        //Wisp
        if (wispSpawned < maxWispPerSet)
        {
            if (shouldSpawnWisp)
            {
                Wisp wisp = SpawnWisp(platformToSpawnWisp);
                Platform plat = platformToSpawnWisp.GetComponent<Platform>();
                plat.PlatformType = PlatformType.Simple;
                plat.ChangeRuneSprite(GameGlobel.Instance.wispRuneSprite);
                plat.runeRenderer.transform.localScale = new Vector3(0.1818134f, 0.1818134f, 0.1818134f);
                plat.runeRenderer.color = wisp.GetColor();
            }
        }
    }

    Wisp SpawnWisp (GameObject platformGO)
    {
        Vector3 calWispPos = new Vector3(platformGO.transform.position.x, platformGO.transform.position.y, platformGO.transform.position.z);
        GameObject wispGO = Instantiate(wispPrefab, calWispPos, Quaternion.identity) as GameObject;

        wispGO.name += "_" + platformGO.name;
        wispGO.transform.parent = platformGO.transform;

        wispSpawned += 1;
        if (printDebug)
        {
            print(wispGO.name);
        }
        return wispGO.GetComponent<Wisp>();
    }

    bool ShouldSpawnWisp()
    {
        float chance = Random.Range(0,101);
        if(spawnChance >= chance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateLeaveAreaGO(Vector3 spawnPos)
    {
        leaveAreaGO = Instantiate(leaveAreaPrefab, spawnPos, Quaternion.identity) as GameObject;
        leaveAreaGO.name += "_" + name;
        leaveAreaGO.transform.parent = this.transform;
    }

    void ShiftSetUp ()
    {
        shiftSetAmount += shiftSetAmountBy;
        transform.position = new Vector3(0, shiftSetAmount, 0);
    }

    public void CleanUpPlatformInstances()
    {
        for (int i = 0; i < PlatformInstances.Length; i++)
        {
            if (PlatformInstances[i])
            {
                Destroy(PlatformInstances[i].gameObject);
            }
        }
        Destroy(leaveAreaGO);
    }

    void ResetSelf ()
    {
        InitData();
    }
}
