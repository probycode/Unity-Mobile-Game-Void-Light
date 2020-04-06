using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnlockButtonUI : ViewUIButton {

    private void Awake()
    {
        base.InitBtn();
        InitSubEvents();
    }

    private void InitSubEvents ()
    {

    }

    protected override void ButtonAction()
    {
        if(ViewController.currentViewUI.IsBusy)
        {
            print("View Is Busy");
            return;
        }
        if (ItemManager.IsAllItemsUnlocked() == false)
        {
            PurchaseState purchaseState = PurchaseManager.PurchaseItem(GameGlobel.BUY_COLOR_PRICE);
            if (purchaseState == PurchaseState.Success)
            {
                List<Item> items = ItemManager.UnlockRandomItems(GameGlobel.AMOUNT_OF_COLORS_TO_UNLOCK);
                PurchaseManager.OnPurchaseInitiated(items);
            }
            else if (purchaseState == PurchaseState.NotEnough)
            {
                //string message = "Not enough wisps" + "\n" + "\n" + "Would you like to watch an ad for " + GameGlobel.WATCH_AD_EARN_AMOUNT + "wisps?";
                Action OnConfirmed = (() =>
                {
                    AdsManager.ShowRewardedAd();
                });

                Action OnCanceled = (() =>
                {

                });

                PopUpViewController.CreatePopUpNotEnoughWisps(OnConfirmed, OnCanceled);
            }
        }
    }
}
