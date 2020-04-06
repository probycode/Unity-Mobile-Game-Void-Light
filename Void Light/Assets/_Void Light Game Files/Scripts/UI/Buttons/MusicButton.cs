using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : ViewUIButton {

    public Text btn_Title;
    public Image muteIcon;

    private void Awake()
    {
        base.InitBtn();
        MusicManager.MusicStatChanged += MusicManager_MusicStatChanged;
        SetBtn();
    }

    protected override void ButtonAction()
    {
        MusicManager.OnMusicStatChanged();
    }

    void SetBtn()
    {
        if (MusicManager.IsMusicOn == false)
        {
            btn_Title.text = "Music : Off";
            muteIcon.enabled = true;
        }
        else if (MusicManager.IsMusicOn == true)
        {
            btn_Title.text = "Music : On";
            muteIcon.enabled = false;
        }
    }

    private void OnDestroy()
    {
        MusicManager.MusicStatChanged -= MusicManager_MusicStatChanged;
    }

    private void MusicManager_MusicStatChanged()
    {
        SetBtn();
    }
}
