using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : ViewUIButton {

    public float delayStart;
    public bool _Pressed;

    private void Awake()
    {
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        StartCoroutine(delayAction());
    }

    private void OnEnable()
    {
        _Pressed = false;
        base.InitBtn();
    }

    IEnumerator delayAction ()
    {
        if(_Pressed)
        {
            btn.onClick.RemoveAllListeners();
            yield break;
        }
        _Pressed = true;

        yield return new WaitForSeconds(delayStart);
        GameManager.OnGameStart();         
    }
}
