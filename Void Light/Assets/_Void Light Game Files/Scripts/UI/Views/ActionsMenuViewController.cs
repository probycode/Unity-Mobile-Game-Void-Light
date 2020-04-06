using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsMenuViewController : ViewUI {

    public static ActionsMenuViewController Instance;
    public Text wispAmountUI;
    public Image baseSR;
    public Image auraSR;

    private void Awake()
    {
        Instance = this;
        InitSubEvents();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
    }

    public void ViewController_UIUpdate ()
    {
        OnUpdateUI();
    }

    public override void OnUpdateUI()
    {
        wispAmountUI.text = ScoreManager.WispesCollected.ToString();

        baseSR.color = Utilities.FloatToColor(GameManager.playerData.baseColor);
        auraSR.color = Utilities.FloatToColor(GameManager.playerData.auraColor);
    }

    public void InitGraphics ()
    {

    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void ButtonActionInput(UIButtonsActions UIButtonsActions)
    {
        throw new NotImplementedException();
    }
}
