using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeColorMode : ViewUIButton {

    public ColorMode colorMode;
    public Transform pointerPos;
    private Button m_Button;

    private void Awake()
    {
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        Action OnComplete = (() =>
        {
            
        });

        CustomizePlayerViewController.Instance.ColorMode = colorMode;
        CustomizePlayerViewController.Instance.pointerUI.SetPointer(pointerPos.position, OnComplete);
    }
}
