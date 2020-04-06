using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphicsController : MonoBehaviour {

    public GameObject particleBase;
    public GameObject particleAura;
    public GameObject graphics;
    public Color baseColor;
    public Color auraColor;

    private void Awake()
    {
        CustomizePlayerViewController.ChangedColor += CustomizePlayerViewController_ChangedColor;
        GameManager.GameLoaded += GameManager_GameLoaded;
    }

    private void GameManager_GameLoaded()
    {
        if (GameManager.playerData != null)
        {
            baseColor = Utilities.FloatToColor(GameManager.playerData.baseColor);
            auraColor = Utilities.FloatToColor(GameManager.playerData.auraColor);
            InitGraphics();
        }
    }

    private void CustomizePlayerViewController_ChangedColor()
    {
        InitGraphics();
    }

    private void OnEnable()
    {
        if (GameManager.playerData != null)
        {
            baseColor = Utilities.FloatToColor(GameManager.playerData.baseColor);
            auraColor = Utilities.FloatToColor(GameManager.playerData.auraColor);
            InitGraphics();
        }
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
        CustomizePlayerViewController.ChangedColor -= CustomizePlayerViewController_ChangedColor;
    }

    void InitGraphics()
    {
        particleBase.GetComponent<ParticleSystem>().startColor = baseColor;
        particleAura.GetComponent<ParticleSystem>().startColor = auraColor;
    }
}
