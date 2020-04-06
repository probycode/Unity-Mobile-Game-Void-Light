using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispsInBackroundController : MonoBehaviour {

    public GameObject wispPrefab;

    private void Awake()
    {
        InitSubEvents();
    }

    void InitSubEvents ()
    {
        GameManager.GameLoaded += GameManager_GameLoaded;
        Wisp.WispCollected += Wisp_WispCollected;
    }

    private void GameManager_GameLoaded()
    {
        InitBackround();
    }

    private void Wisp_WispCollected()
    {
        AddWisp();
    }

    public void InitBackround ()
    {
        for (int i = 0; i < GameManager.playerData.wispsCollected; i++)
        {
            AddWisp();
        }
    }

    public void AddWisp ()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
        Wisp.WispCollected -= Wisp_WispCollected;
    }
}
