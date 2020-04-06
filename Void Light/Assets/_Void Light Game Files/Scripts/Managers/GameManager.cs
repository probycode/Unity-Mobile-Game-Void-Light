using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void GameEventHandler();
    public static event GameEventHandler GameOver;
    public static event GameEventHandler GameRestart;
    public static event GameEventHandler GameStart;
    public static event GameEventHandler LeftArea;
    public static event GameEventHandler GameInitiatingSave;
    public static event GameEventHandler GameSaved;
    public static event GameEventHandler GameLoaded;
    public static event GameEventHandler GameSavePlayerCurrentStreak;


    public static GameManager Instance;
    public static PlayerController playerGO;
    public static PlayerData playerData;
    public GameObject playerPrefab;
    public GameObject sceneGO;
    public bool itemManagerDebug = false;

    private void Awake()
    {
        Instance = this;
        InitSubEvents();
    }

    private void InitSubEvents()
    {
        GameManager.GameStart += GameManager_GameStart;
        PlayerController.PlayerDead += PlayerController_PlayerDead;
        ItemManager.ItemUnlocked += ItemManager_ItemUnlocked;
    }

    private void ItemManager_ItemUnlocked(Item item)
    {
        SaveGame();
    }

    private void PlayerController_PlayerDead()
    {
        Destroy(playerGO.gameObject);
        InitGame();
    }

    private void Start()
    {
        LoadGame();
    }

    private void GameManager_GameStart()
    {
        InitGame();
    }

    private void OnDestroy()
    {
        GameManager.GameStart -= GameManager_GameStart;
        PlayerController.PlayerDead -= PlayerController_PlayerDead;
        ItemManager.ItemUnlocked -= ItemManager_ItemUnlocked;
    }

    void InitGame ()
    {
        playerGO = Instantiate(playerPrefab).gameObject.GetComponent<PlayerController>();
        playerGO.gameObject.transform.parent = sceneGO.transform;
    }

    #region Events
    public static void OnGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }

    public static void OnGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }

    public static void OnGameRestart()
    {
        if (GameRestart != null)
        {
            GameRestart();
        }
    }

    public static void OnLeftArea()
    {
        if (LeftArea != null)
        {
            LeftArea();
        }
    }

    public static void OnGameInitiatingSave()
    {
        if (GameInitiatingSave != null)
        {
            GameInitiatingSave();
        }
    }

    public static void OnGameSaved()
    {
        if (GameSaved != null)
        {
            GameSaved();
        }
    }

    public static void OnGameLoaded()
    {
        if (GameLoaded != null)
        {
            GameLoaded();
        }
    }

    public static void OnGameSavePlayerCurrentStreak()
    {
        
        if (GameSavePlayerCurrentStreak != null)
        {
            GameSavePlayerCurrentStreak();
        }
    }
    #endregion Events

    //Not Static so button can use method
    public void SavePlayerCurrentScore(bool sentEventMessage = true)
    {
        playerData.score = ScoreManager.Score;

        if (sentEventMessage)
        {
            Destroy(playerGO.gameObject);
            OnGameSavePlayerCurrentStreak();
        }

        SaveGame();
    }

    public static void SaveGame()
    {
        GameInitiatingSave();
        playerData.itemManagerData = ItemManager.GetItemManagerData();
        ItemManager.shouldDebug = GameManager.Instance.itemManagerDebug;
        SaveLoadManager.SavePlayer(playerData);
        playerData.playerGameData = GameData.GetData();
        //print("Game Saved");
    }

    public static void LoadGame()
    {
        playerData = new PlayerData();

        if (SaveLoadManager.LoadPlayer() != null)
        {
            PlayerData loadedData = SaveLoadManager.LoadPlayer();

            playerData.highscore = loadedData.highscore;
            playerData.score = loadedData.score;
            playerData.proggress = loadedData.proggress;
            playerData.isMusicOn = loadedData.isMusicOn;
            playerData.isSFXOn = loadedData.isSFXOn;
            playerData.loopIndex = loadedData.loopIndex;
            playerData.auraColor = loadedData.auraColor;
            playerData.baseColor = loadedData.baseColor;
            playerData.wispsCollected = loadedData.wispsCollected;
            playerData.itemManagerData = loadedData.itemManagerData;
            playerData.tillAdCounter = loadedData.tillAdCounter;
            playerData.playerGameData = loadedData.playerGameData;

            //Loaded Items
            ItemManager.LoadItemManagerData(playerData.itemManagerData);
            GameData.LoadData(playerData.playerGameData);

            print("GameManager: Game Loaded");
        }
        else
        {
            print("GameManager: No Game File Found");
        }
        GameData.Init();
        OnGameLoaded();
    }

    public void DeleteGame ()
    {
        playerData = new PlayerData();
        ItemManager.LockAllItems();
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
