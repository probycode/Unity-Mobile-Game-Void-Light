using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpViewAds : PopUpView {


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
