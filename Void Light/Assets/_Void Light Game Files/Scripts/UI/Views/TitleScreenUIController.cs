using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUIController : ViewUI
{
    [Header("UI Inits")]
    public Text highScoreUI;
    public Text currentStreak;
    public Image tapToPlay_Image;
    public Sprite tapToContinue_SPR;
    public Sprite tapToPlay_SPR;

    private void Awake()
    {
        InitSubEvents();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
        GameManager.GameLoaded += GameManager_GameLoaded;
        GameManager.GameSavePlayerCurrentStreak += GameManager_GameSavePlayerCurrentStreak;
    }

    private void GameManager_GameSavePlayerCurrentStreak()
    {
        InitTapToPlayText();
    }

    private void GameManager_GameLoaded()
    {
        InitTapToPlayText();
    }

    public override void OnUpdateUI()
    {
        highScoreUI.text = ScoreManager.HighScore.ToString();
        currentStreak.text = ScoreManager.Score.ToString();
    }

    void InitTapToPlayText()
    {
        if (GameManager.playerData.score != 0)
        {
            tapToPlay_Image.sprite = tapToContinue_SPR;
        }
        else
        {
            tapToPlay_Image.sprite = tapToPlay_SPR;
        }
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
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
