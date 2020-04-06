using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUpView : MonoBehaviour {

    public Action OnConfirmed;
    public Action OnCanceled;

    abstract public void OnButtonActionInput(UIButtonsActions UIButtonsActions);
    abstract public void ClosePopUp();
}
