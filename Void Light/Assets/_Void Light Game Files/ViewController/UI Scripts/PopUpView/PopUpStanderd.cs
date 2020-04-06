using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpStanderd : PopUpView {

    [SerializeField]
    private Text message_TEXT;

    private string message;

    public void Init(string message, Action OnConfirmed, Action OnCanceled)
    {
        this.OnConfirmed = OnConfirmed;
        this.OnCanceled = OnCanceled;
        this.message = message;

        InitGraphics();
    }

    public void InitGraphics ()
    {
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
    }

    public override void ClosePopUp()
    {
        Destroy(gameObject);
    }
}
