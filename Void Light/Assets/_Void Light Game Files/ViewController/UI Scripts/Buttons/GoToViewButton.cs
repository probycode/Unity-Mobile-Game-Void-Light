using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToViewButton : ViewUIButton {

    public float delayStart;
    public View desiredView;
    private bool _Pressed;

    private void Awake()
    {
        base.InitBtn();
    }

    protected override void ButtonAction()
    {
        if (ViewController.currentViewUI.IsBusy == false)
        {
            StartCoroutine(delayAction());
        }
        else
        {
            print("ButtonAction: View is busy");
        }
    }

    private void OnEnable()
    {
        _Pressed = false;
        base.InitBtn();
    }

    IEnumerator delayAction()
    {
        if (_Pressed)
        {
            btn.onClick.RemoveAllListeners();
            yield break;
        }
        _Pressed = true;
        yield return new WaitForSeconds(delayStart);
        ViewController.ShowView(desiredView);
    }
}
