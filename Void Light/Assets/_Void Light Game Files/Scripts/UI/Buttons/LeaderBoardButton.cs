using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardButton : ViewUIButton {

    private void Awake()
    {
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        ViewHighScore();
    }

    public void ViewHighScore()
    {
        if(GooglePlayManager.isAuthenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            //Ask user to sign in through popup

            string message = "You are not signed in. " + "\n" + "\n" + 
                "Would you like to sign in to google play?";

            System.Action OnConfirmed = ( () => 
            {
               System.Action SignInSuccess = (() => 
               {
                   ViewHighScore();
               });
               System.Action SignInFailed = (() =>
               {
                   message = "Error could not sign in.";

                   PopUpViewController.CreatePopUpMessage(message, (() =>
                   {

                   }));
               });

                GooglePlayManager.SignIn(SignInSuccess, SignInFailed);
            } );

            System.Action OnCanceled = (() =>
            {

            });

            PopUpViewController.CreatePopUpStanderd(message, OnConfirmed, OnCanceled);
        }
    }

}
