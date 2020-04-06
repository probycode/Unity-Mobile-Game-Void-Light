using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpViewMessage : PopUpView
{
    public Text message_TEXT;
    private Action OnClose;
    private string message;

    public void Init(string message,Action OnClose)
    {
        this.OnClose = OnClose;
        this.message = message;
        message_TEXT.text = message;
    }

    public override void OnButtonActionInput(UIButtonsActions UIButtonsActions)
    {
        if (UIButtonsActions == UIButtonsActions.Confirm)
        {
            OnConfirmed();
            ClosePopUp();
        }
        else if (UIButtonsActions == UIButtonsActions.Cancel)
        {
            OnCanceled();
            ClosePopUp();
        }
        else if (UIButtonsActions == UIButtonsActions.Close)
        {
            OnClose();
            ClosePopUp();
        }
    }

    public override void ClosePopUp()
    {
        Destroy(gameObject);
    }
}
