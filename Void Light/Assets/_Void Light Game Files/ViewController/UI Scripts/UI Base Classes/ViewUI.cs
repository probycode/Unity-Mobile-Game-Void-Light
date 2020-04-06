using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ViewUI : MonoBehaviour{

    public View view;
    protected bool isBusy = false;

    public bool IsBusy
    {
        get
        {
            return isBusy;
        }
    }

    private void Awake()
    {
        InitSubEvents();
    }

    public void InitSubEvents()
    {
        ViewController.UIUpdate += ViewController_UIUpdate;
    }

    private void ViewController_UIUpdate()
    {
        OnUpdateUI();
    }

    abstract public void OnUpdateUI();

    abstract public void Close();

    abstract public void Open();

    abstract public void ButtonActionInput(UIButtonsActions UIButtonsActions);

    private void OnDestroy()
    {
        ViewController.UIUpdate -= ViewController_UIUpdate;
    }
}
