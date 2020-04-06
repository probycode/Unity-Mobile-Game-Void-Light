using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIButtonsActions
{
    Confirm,
    Cancel,
    Close
}

public class ActionButton : ViewUIButton
{

    private PopUpView _popUpListener;
    public UIButtonsActions buttonsActions;

    public PopUpView PopUpListener
    {
        get
        {
            return _popUpListener;
        }
    }

    private void Awake()
    {
        _popUpListener = transform.GetComponentInParent<PopUpView>();
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        _popUpListener.OnButtonActionInput(buttonsActions);
    }
}
