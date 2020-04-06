using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour {

    public SpriteRenderer[] spriteRenderers;
    public GameObject tapToLaunchEffectPrefab;
    private GameObject tapToLaunchEffectInstance;

    private SortingLayer inGameLayer;
    private SortingLayer inTitleScreenLayer;

    private void Awake()
    {
        InitSubEvents();
    }

    void ChangeSpriteLayer (string layer)
    {
        foreach (var spite in spriteRenderers)
        {
            spite.sortingLayerName = layer;
        }
    }

    void InitSubEvents()
    {
        ViewController.ViewChanged += ViewController_ViewChanged;
        GameManager.GameStart += GameManager_GameStart;
        Platform.ReachedLastPlat += Platform_ReachedLastPlat;
        PlayerController.PlayerReachedStartPlat += PlayerController_PlayerReachedStartPlat;
    }

    private void Platform_ReachedLastPlat(GameObject platformGO)
    {
        if (platformGO.GetComponent<Platform>().isHighScorePlat)
        {
            Destroy(platformGO.GetComponent<PlatformUIController>().highStreakInstance);
        }
        GameObject tapToLaunchEffectInstance = Instantiate(tapToLaunchEffectPrefab) as GameObject;
        tapToLaunchEffectInstance.transform.SetParent(platformGO.transform, true);
        tapToLaunchEffectInstance.transform.localPosition = Vector3.zero;
    }

    private void PlayerController_PlayerReachedStartPlat()
    {
        Destroy(tapToLaunchEffectInstance);
    }


    private void GameManager_GameStart()
    {
        gameObject.SetActive(true);
        ChangeSpriteLayer(GameGlobel.DEFAULT_LAYER);
    }

    private void OnDestroy()
    {
        ViewController.ViewChanged -= ViewController_ViewChanged;
        GameManager.GameStart -= GameManager_GameStart;
        Platform.ReachedLastPlat -= Platform_ReachedLastPlat;
        PlayerController.PlayerReachedStartPlat -= PlayerController_PlayerReachedStartPlat;
    }

    private void ViewController_ViewChanged(View view)
    {
        if(view == View.Title || view == View.InGame)
        {
            gameObject.SetActive(true);
            if(view == View.Title)
            {
                ChangeSpriteLayer(GameGlobel.DARKEFFECT_LAYER);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
