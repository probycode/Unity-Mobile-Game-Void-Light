using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEffectUI : MonoBehaviour {

    public SpriteRenderer darkEffect;

    private void Awake()
    {
        ViewController.ViewChanged += ViewController_ViewChanged;
    }

    private void ViewController_ViewChanged(View view)
    {
        if (view == View.InGame || view == View.CustomizePlayer || view == View.CodexSelect || view == View.Codex)
        {
            darkEffect.enabled = false;
        }
        else
        {
            darkEffect.enabled = true;
        }
    }
}
