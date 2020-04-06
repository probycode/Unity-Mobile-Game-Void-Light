using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;


public class GooglePlayManager : MonoBehaviour {

    public static GooglePlayManager Instance;
    public delegate void GooglePlayEventHandler(bool success);
    public static event GooglePlayEventHandler Initialized;

    public static bool isAuthenticated;

    public bool DebugLogEnabled = true;

    public static void OnInitialized(bool success)
    {
        if (Initialized != null)
        {
            Initialized(success);
        }
    }

    private void Awake()
    {
        Instance = this;

        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        //PlayGamesPlatform.InitializeInstance(config);
        //// recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = DebugLogEnabled;
        //// Activate the Google Play Games platform
        //PlayGamesPlatform.Activate();

        SignIn();

        /*string message = "\n" + "\n" + "Would you like to sign into your google play account?";
        Action OnConfirm = (() =>
        {
            SignIn();
        });

        Action OnCancel = ( () =>
        {

        });

        PopUpViewController.CreatePopUpStanderd(message, OnConfirm, OnCancel);*/
    }

    public static void SignIn ()
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) =>
        {
            isAuthenticated = success;
            if (GooglePlayManager.Instance.DebugLogEnabled)
            {
                print("Signed in");
            }
            OnInitialized(success);
        });
    }

    public static void SignIn(System.Action SignInSuccess, System.Action SignInFailed)
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) =>
        {
            isAuthenticated = success;
            if (GooglePlayManager.Instance.DebugLogEnabled)
            {
                SignInSuccess();
            }
            else
            {
                //SignInFailed();
            }
            OnInitialized(success);
        });
    }

    public void ViewHighScore()
    {
        if (DebugLogEnabled)
        {
            print("ViewHighScore");
        }
        Social.ShowLeaderboardUI();
    }

    public void ViewAchievements()
    {
        if (DebugLogEnabled)
        {
            print("ViewAchievements");
        }
        Social.ShowAchievementsUI();
    }
}
