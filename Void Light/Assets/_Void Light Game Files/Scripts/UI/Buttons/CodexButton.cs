using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexButton : ViewUIButton {

    public CodexData codexData;

    private void Awake()
    {
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        ViewController.ShowRuneCodex(codexData);
    }
}
