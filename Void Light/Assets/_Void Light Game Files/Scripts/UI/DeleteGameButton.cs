using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameButton : ViewUIButton {

    protected override void ButtonAction()
    {
        GameManager.Instance.DeleteGame();   
    }
}
