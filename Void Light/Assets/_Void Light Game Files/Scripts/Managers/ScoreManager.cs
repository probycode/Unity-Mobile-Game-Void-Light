using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public delegate void ScoreEventHandler();
    public delegate void ScoreAddedEventHandler(int score);
    public static event ScoreEventHandler BeatHighScore;
    public static event ScoreAddedEventHandler ScoreAdded;

    public static ScoreManager Instance;
    public static int playRoundsTillAds = 8;
    private static int highScore;
    private static int playedRounds;

    private static int score;
    private static int wispesCollected;

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            CompareScoreWithHighScore();
            ///CheckIfGotAchievment();
        }
    }

    public static int WispesCollected
    {
        get
        {
            return wispesCollected;
        }

        set
        {
            wispesCollected = value;
            GameManager.playerData.wispsCollected = wispesCollected;
            ViewController.OnUIUpdate();
        }
    }

    public static int HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }

    public static void OnBeatHighSCore()
    {
        if (BeatHighScore != null)
        {
            BeatHighScore();
        }
    }

    public static void OnScoreAdded(int score)
    {
        if (ScoreAdded != null)
        {
            ScoreAdded(score);
        }
    }

    void Awake()
    {
        SubToEvents();
        Instance = this;
    }

    void SubToEvents()
    {
        GameManager.GameInitiatingSave += GameManager_GameInitiatingSave;
        GameManager.GameOver += GameManager_GameOver;
        GameManager.GameRestart += GameManager_GameRestart;
        PlayerController.PlayerDead += GameManager_PlayerDead;
        GameManager.GameLoaded += GameManager_GameLoaded;
        PlayerController.PlayerReachedPlat += PlayerController_PlayerReachedPlat;
        Wisp.WispCollected += Wisp_WispCollected;
    }

    private void GameManager_GameInitiatingSave()
    {
        if (GameManager.playerData != null)
        {
            GameManager.playerData.wispsCollected = wispesCollected;
        }
    }

    private void GameManager_GameLoaded()
    {
        HighScore = GameManager.playerData.highscore;
        Score = GameManager.playerData.score;
        wispesCollected = GameManager.playerData.wispsCollected;
        ViewController.OnUIUpdate();
    }

    private void PlayerController_PlayerReachedPlat()
    {
        Score += 1;
        OnScoreAdded(score);
        ViewController.OnUIUpdate();
    }

    private void Wisp_WispCollected()
    {
        WispesCollected += 1;
        ViewController.OnUIUpdate();
    }

    private void GameManager_PlayerDead()
    {
        ResetValues();
    }

    private void GameManager_GameRestart()
    {
        ResetValues();
    }

    private void GameManager_GameOver()
    {
        print(playedRounds + " : " + playRoundsTillAds);
        if (playedRounds >= playRoundsTillAds)
        {
            playedRounds = 0;
            //AdsManager.ShowAd();
        }
        else
        {
            playedRounds++;
        }
    }

    private void ResetValues()
    {
        Score = 0;
        GameManager.Instance.SavePlayerCurrentScore(false);
        ViewController.OnUIUpdate();
    }

    void OnDestroy()
    {
        GameManager.GameInitiatingSave -= GameManager_GameInitiatingSave;
        GameManager.GameOver -= GameManager_GameOver;
        GameManager.GameRestart -= GameManager_GameRestart;
        PlayerController.PlayerDead -= GameManager_PlayerDead;
        PlayerController.PlayerReachedPlat -= PlayerController_PlayerReachedPlat;
        Wisp.WispCollected -= Wisp_WispCollected;
        GameManager.GameLoaded -= GameManager_GameLoaded;
    }

    private static void CompareScoreWithHighScore ()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            AssignDataToPlayer();
            GameManager.SaveGame();
            OnBeatHighSCore();
            
            Social.ReportScore(score, AndroidGPGSIds.leaderboard_high_streak, (bool success) => 
            {
                print("High Schore Uploaded");
            });
        }
    }

    static void AssignDataToPlayer ()
    {
        GameManager.playerData.highscore = HighScore;
    }
}
