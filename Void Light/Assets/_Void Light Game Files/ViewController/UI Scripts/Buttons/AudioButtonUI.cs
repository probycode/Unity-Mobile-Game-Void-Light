using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonUI : ViewUIButton {

    public float volume = 0.3f;
    public bool overrideDefaultAudio;
    public AudioClip[] clips;
    private AudioSource audioSource;
    public bool isMute = false;

    private void Awake()
    {
        base.InitBtn();
        if(clips.Length >= 1)
        {
            overrideDefaultAudio = true;
        }
        InitSubEvents();
    }

    public void InitSubEvents ()
    {
        
    }

    public void PlaySound ()
    {
        if(isMute)
        {
            print("Is muted");
            return;
        }
        if (ViewController.currentViewUI.IsBusy)
        {
            print("Cant PlaySound cus view is busy");
            return;
        }
        if (clips != null && overrideDefaultAudio)
        {
            foreach (var clip in clips)
            {
                ViewController.PlaySound(clip, true, volume);
            }
        }
        else
        {
            ViewController.PlaySound(new AudioClip(), false, volume);
        }
    }

    protected override void ButtonAction()
    {
        PlaySound();
    }

    private void OnEnable()
    {
        btn.onClick.AddListener(() => PlaySound());
    }

    public void Mute ()
    {
        isMute = true;
    }

    public void UnMute()
    {
        isMute = false;
    }
}
