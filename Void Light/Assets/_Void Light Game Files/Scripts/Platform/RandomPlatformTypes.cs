using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformTypes : MonoBehaviour {

    public bool debuggingStuff;

    [Header("Random Platform Type for set")]
    public PlatformType[] platformTypeSet;

    [Header("Rune Types")]
    public float chanceForSimple;
    public float chanceForRanSpeed;
    public float chanceForJuke;
    public float chanceForFade;
    public float chanceForRotate;

    [Header("Can Spawn Rune Types")]
    public bool canSpawnSimpleRune = true;
    public bool canSpawnRanSpeedRune = false;
    public bool canSpawnJukeRune = false;
    public bool canSpawnFadeRune = false;
    public bool canSpawnWildRune = false;

    public static int proggress;

    private PlatformSpawner platformSpawner;

    private void Awake()
    {
        GameManager.LeftArea += GameManager_LeftArea;
        PlayerController.PlayerDead += PlayerController_PlayerDead;
        GameManager.GameStart += GameManager_GameStart;
        GameManager.GameLoaded += GameManager_GameLoaded;
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    private void GameManager_GameLoaded()
    {
        proggress = GameManager.playerData.proggress;
    }

    private void GameManager_GameStart()
    {
        SetProggress();
    }

    private void SetProggress()
    {
        if (proggress > 0)
        {
            canSpawnRanSpeedRune = true;
        }
        if (proggress > 1)
        {
            canSpawnFadeRune = true;
        }
        if (proggress > 2)
        {
            canSpawnJukeRune = true;
        }
        if (proggress > 3)
        {
            canSpawnWildRune = true;
        }

        GameManager.playerData.proggress = proggress;
    }

    private void PlayerController_PlayerDead()
    {
        ResetValues();
        GameManager.playerData.proggress = 0;
        GameManager.SaveGame();
    }

    private void OnDestroy()
    {
        GameManager.LeftArea -= GameManager_LeftArea;
        PlayerController.PlayerDead -= PlayerController_PlayerDead;
        GameManager.GameStart -= GameManager_GameStart;
        GameManager.GameLoaded -= GameManager_GameLoaded;
    }

    private void GameManager_LeftArea()
    {
        proggress += 1;
        SetProggress();
        GameManager.playerData.proggress = proggress;
        GameManager.SaveGame();
    }

    //Make it that some percent to spawn norm plat but if it doesnt then make a random plat of other type
    public PlatformType[] CreateSet()
    {
        platformTypeSet = new PlatformType[platformSpawner.AmountOfPlatformPerSet];

        for (int i = 0; i < platformSpawner.AmountOfPlatformPerSet; i++)
        {
            float chance = Random.Range(0, 101);

            if (chanceForSimple >= chance)
            {
                platformTypeSet[i] = PlatformType.Simple;
            }
            else if (chanceForRanSpeed >= chance && canSpawnRanSpeedRune)
            {
                platformTypeSet[i] = PlatformType.RandomSpeed;
            }
            else if (chanceForJuke >= chance && canSpawnJukeRune)
            {
                platformTypeSet[i] = PlatformType.Juke;
            }
            else if (chanceForFade >= chance && canSpawnFadeRune)
            {
               platformTypeSet[i] = PlatformType.Fade;
                if (i == 0)
                {
                    platformTypeSet[i] = PlatformType.Simple;
                }
            }
            else if (chanceForRotate >= chance && canSpawnWildRune)
            {
                platformTypeSet[i] = PlatformType.Wild;
                if (i == 0)
                {
                    platformTypeSet[i] = PlatformType.Wild;
                }
            }
        }
        return platformTypeSet;
    }

    void ResetValues ()
    {
        if(debuggingStuff)
        {
            return;
        }
        proggress = 0;
        canSpawnRanSpeedRune = false;
        canSpawnJukeRune = false;
        canSpawnFadeRune = false;
        canSpawnWildRune = false;
    }
}
