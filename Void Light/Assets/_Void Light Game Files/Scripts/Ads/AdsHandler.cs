using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsHandler : MonoBehaviour {

    public delegate void AdsEventHandler();
    public static event AdsEventHandler aboutToPlayAd;

    public int playAdAt;
    public static int tillAdCounter;

    private PopUpView popUpView;

    private void Awake()
    {
        AdsManager.Init();
        InitSubEvent();
    }

    void InitSubEvent ()
    {
        PlayerController.PlayerDead += PlayerController_PlayerDead;
        GameManager.GameLoaded += GameManager_GameLoaded;
        GameManager.GameInitiatingSave += GameManager_GameInitiatingSave;
    }

    private void GameManager_GameInitiatingSave()
    {
        GameManager.playerData.tillAdCounter = tillAdCounter;
    }

    private void GameManager_GameLoaded()
    {
        tillAdCounter = GameManager.playerData.tillAdCounter;
    }

    private void PlayerController_PlayerDead()
    {
        tillAdCounter++;
        if(tillAdCounter >= playAdAt)
        {
            if (ScoreManager.WispesCollected < GameGlobel.SKIP_AD_PRICE)
            {
                AdsManager.ShowAd();
                ResetValues();
                return;
            }

            Action OnConfiremd = () => 
            {
                if(ScoreManager.WispesCollected >= GameGlobel.SKIP_AD_PRICE)
                {
                    ScoreManager.WispesCollected -= GameGlobel.SKIP_AD_PRICE;
                    ResetValues();
                }
                else
                {
                    print("Not enough Wisps");
                    AdsManager.ShowAd();
                }
            };

            Action OnCanceled = () => 
            {
                AdsManager.ShowAd();
            };

            popUpView = PopUpViewController.CreatePopUpAds(OnConfiremd, OnCanceled)as PopUpView;

            ResetValues();
        }
    }

    void ResetValues()
    {
        tillAdCounter = 0;
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDead -= PlayerController_PlayerDead;
    }
}
