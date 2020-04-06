using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUIController : ViewUI{

    private void Awake()
    {
        InitSubEvents();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
    }

    private void ViewController_UIUpdate()
    {
        OnUpdateUI();
    }

    override public void OnUpdateUI()
    {
        
    }

    private void OnDestroy()
    {
        
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
