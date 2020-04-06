using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopUpViewController : MonoBehaviour {

    public static PopUpViewController Instance;
    public static bool PopUpExistInGame;
    public GameObject PopUpAdsPrefab;
    public GameObject PopUpAdsNotEnoughWispsPrefab;
    public GameObject PopUpMessagePrefab;
    public GameObject PopUpStanderdPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public static PopUpStanderd CreatePopUpStanderd(string message,Action OnConfirmed, Action OnCanceled)
    {
        PopUpStanderd popUpInstance = Instantiate(Instance.PopUpStanderdPrefab).GetComponent<PopUpStanderd>();
        popUpInstance.transform.SetParent(Instance.transform, false);

        OnConfirmed += (() => { PopUpExistInGame = false; });
        OnCanceled += (() => { PopUpExistInGame = false; });

        popUpInstance.Init(message, OnConfirmed, OnCanceled);

        PopUpExistInGame = true;
        return popUpInstance;
    }

    public static PopUpViewAds CreatePopUpAds(Action OnConfirmed, Action OnCanceled)
    {
        PopUpViewAds popUpInstance = Instantiate(Instance.PopUpAdsPrefab).GetComponent<PopUpViewAds>();
        popUpInstance.transform.SetParent(Instance.transform, false);

        OnConfirmed += (() => { PopUpExistInGame = false; });
        OnCanceled += (() => { PopUpExistInGame = false; });

        popUpInstance.OnConfirmed = OnConfirmed;
        popUpInstance.OnCanceled = OnCanceled;

        PopUpExistInGame = true;
        return popUpInstance;
    }

    public static PopUpViewAds CreatePopUpNotEnoughWisps(Action OnConfirmed, Action OnCanceled)
    {
        PopUpViewAds popUpInstance = Instantiate(Instance.PopUpAdsNotEnoughWispsPrefab).GetComponent<PopUpViewAds>();
        popUpInstance.transform.SetParent(Instance.transform, false);

        OnConfirmed += (() => { PopUpExistInGame = false; });
        OnCanceled += (() => { PopUpExistInGame = false; });

        popUpInstance.OnConfirmed = OnConfirmed;
        popUpInstance.OnCanceled = OnCanceled;

        PopUpExistInGame = true;
        return popUpInstance;
    }

    public static PopUpViewMessage CreatePopUpMessage(string message ,Action OnClose)
    {
        PopUpViewMessage popUpInstance = Instantiate(Instance.PopUpMessagePrefab).GetComponent<PopUpViewMessage>();
        popUpInstance.transform.SetParent(Instance.transform, false);

        OnClose += ( () => { PopUpExistInGame = false; });

        popUpInstance.Init(message, OnClose);

        PopUpExistInGame = true;
        return popUpInstance;
    }
}
