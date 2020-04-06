using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSFXButton : ViewUIButton {

    public Text btn_Title;
    public Image muteIcon;

    private void Awake()
    {
        base.InitBtn();
        SFXManager.SFXStatChanged += SFXManager_SFXStatChanged;
        GameManager.GameLoaded += GameManager_GameLoaded;
        ViewController.ViewChanged += ViewController_ViewChanged;
        SetBtn();
    }

    private void ViewController_ViewChanged(View view)
    {
        if(view == View.Options)
        {
            SetBtn();
        }
    }

    private void GameManager_GameLoaded()
    {
        SetBtn();
    }

    protected override void ButtonAction()
    {
        SFXManager.OnSFXStatChanged();
    }

    void SetBtn()
    {
        if (SFXManager.isSFXOn == true)
        {
            btn_Title.text = "SFX : On";
            muteIcon.enabled = false;
        }
        else if (SFXManager.isSFXOn == false)
        {
            btn_Title.text = "SFX : Off";
            muteIcon.enabled = true;
        }
    }

    private void OnDestroy()
    {
        SFXManager.SFXStatChanged -= SFXManager_SFXStatChanged;
    }

    private void SFXManager_SFXStatChanged()
    {
        SetBtn();
    }
}
