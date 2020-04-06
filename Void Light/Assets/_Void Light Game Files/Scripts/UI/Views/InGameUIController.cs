using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : ViewUI{

    public Text scoreUI;
    public Text wispUI;
    public GameObject SaveButton;

    private void Awake()
    {
        InitSubEvents();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
        GameManager.GameStart += GameManager_GameStart;
        PlayerController.PlayerDead += PlayerController_PlayerDead;
        PlayerController.PlayerReachedStartPlat += PlayerController_PlayerReachedStartPlat;
        PlayerController.PlayerLeftedStartPlat += PlayerController_PlayerLeftedStartPlat;
        GameManager.GameSavePlayerCurrentStreak += GameManager_GameSavePlayerCurrentStreak;
    }

    private void GameManager_GameStart()
    {
        SaveButton.SetActive(false);
    }

    private void GameManager_GameSavePlayerCurrentStreak()
    {
        ViewController.ShowView(View.Title);
    }

    private void PlayerController_PlayerLeftedStartPlat()
    {
        SaveButton.SetActive(false);
    }

    private void PlayerController_PlayerReachedStartPlat()
    {
        SaveButton.SetActive(true);
    }

    private void PlayerController_PlayerDead()
    {
        SaveButton.SetActive(false);
    }

    override public void OnUpdateUI()
    {
        wispUI.text = ScoreManager.WispesCollected.ToString();
        scoreUI.text = ScoreManager.Score.ToString();
    }

    private void OnDestroy()
    {
        GameManager.GameStart -= GameManager_GameStart;
        PlayerController.PlayerDead -= PlayerController_PlayerDead;
        PlayerController.PlayerReachedStartPlat -= PlayerController_PlayerReachedStartPlat;
        PlayerController.PlayerLeftedStartPlat -= PlayerController_PlayerLeftedStartPlat;
        GameManager.GameSavePlayerCurrentStreak -= GameManager_GameSavePlayerCurrentStreak;
    }

    override public void Close()
    {
        gameObject.SetActive(false);
    }

    override public void Open()
    {
        gameObject.SetActive(true);
    }

    public override void ButtonActionInput(UIButtonsActions UIButtonsActions)
    {
        throw new NotImplementedException();
    }
}
