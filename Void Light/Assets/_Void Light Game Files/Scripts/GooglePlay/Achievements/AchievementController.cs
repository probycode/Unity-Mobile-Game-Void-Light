using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour {

    public bool debug;

    private void Awake()
    {
        InitSubEvents();
    }

    void InitSubEvents ()
    {
        GameManager.GameLoaded += GameManager_GameLoaded;
        PurchaseManager.PurchaseFinished += PurchaseManager_PurchaseFinished;
        ScoreManager.ScoreAdded += ScoreManager_ScoreAdded;
        GooglePlayManager.Initialized += GooglePlayManager_Initialized;
        MusicManager.MusicLoopIncreased += MusicManager_MusicLoopIncreased;
        CustomizePlayerViewController.ChangedColor += CustomizePlayerViewController_ChangedColor;
        CodexUIController.AllCodexViewed += CodexUIController_AllCodexViewed;
    }

    private void CodexUIController_AllCodexViewed()
    {
        Social.ReportProgress(AndroidGPGSIds.achievement_bookworm, 100.0f, (bool success) =>
        {
            if(debug)
            { 
            print("Unlocked" + AndroidGPGSIds.achievement_bookworm);
            }
        });
    }

    private void CustomizePlayerViewController_ChangedColor()
    {
        Social.ReportProgress(AndroidGPGSIds.achievement_a_new_look, 100.0f, (bool success) =>
        {
            if (debug)
            {
                print("Unlocked" + AndroidGPGSIds.achievement_a_new_look);
            }
        });
    }

    private void MusicManager_MusicLoopIncreased(int loopIndex)
    {
        if(loopIndex == MusicManager.amountOfLoopLayers)
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_the_calling, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked" + AndroidGPGSIds.achievement_the_calling);
                }
            });
        }
    }

    private void GooglePlayManager_Initialized(bool success)
    {
        if (debug)
        {
            print("Init");
        }
    }

    private void ScoreManager_ScoreAdded(int score)
    {
        if (score == 20)
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_no_longer_the_lost, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked" + AndroidGPGSIds.achievement_no_longer_the_lost);
                }
            });
        }
        else if (score == 40)
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_across_the_void, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked" + AndroidGPGSIds.achievement_across_the_void);
                }
            });
        }
        else if (score == 50)
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_evading_darkness, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked" + AndroidGPGSIds.achievement_evading_darkness);
                }
            });
        }
        else if (score == 100)
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_chasing_light, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked" + AndroidGPGSIds.achievement_chasing_light);
                }
            });
        }
    }

    private void PurchaseManager_PurchaseFinished()
    {
        Social.ReportProgress(AndroidGPGSIds.achievement_goodies, 100.0f, (bool success) =>
        {
            if (debug)
            {
                print("Unlocked :" + AndroidGPGSIds.achievement_goodies);
            }
        });

        if(ItemManager.IsAllItemsUnlocked())
        {
            Social.ReportProgress(AndroidGPGSIds.achievement_light_it_up, 100.0f, (bool success) =>
            {
                if (debug)
                {
                    print("Unlocked :" + AndroidGPGSIds.achievement_light_it_up);
                }
            });
        }
    }

    private void GameManager_GameLoaded()
    {
        Social.ReportProgress(AndroidGPGSIds.achievement_play_void_light, 100.0f, (bool success) =>
        {
            if (debug)
            {
                print("Unlocked" + AndroidGPGSIds.achievement_goodies);
            }
        });
    }
}
