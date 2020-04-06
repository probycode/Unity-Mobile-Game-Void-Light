using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PurchaseState
{
    Success,
    Fail,
    NotEnough
}

public static class PurchaseManager {

    public delegate void PurchaseHandler();
    public delegate void PurchaseSuccessHandler(PurchaseState purchaseState);
    public delegate void PurchaseInitiatedHandler(List<Item> Items);

    public static event PurchaseInitiatedHandler PurchaseInitiated;
    public static event PurchaseHandler PurchaseFinished;
    public static event PurchaseSuccessHandler AttemptedAPurchase;

    public static void OnAttemptedAPurchase(PurchaseState purchaseState)
    {
        if (AttemptedAPurchase != null)
        {
            AttemptedAPurchase(purchaseState);
        }
    }

    public static void OnPurchaseInitiated(List<Item> Items)
    {
        if (PurchaseInitiated != null)
        {
            PurchaseInitiated(Items);
        }
    }

    public static void OnPurchaseFinished()
    {
        if (PurchaseFinished != null)
        {
            PurchaseFinished();
        }
    }

    public static PurchaseState PurchaseItem (int price)
    {
        PurchaseState PurchaseState;

        if (ScoreManager.WispesCollected >= price)
        {
            ScoreManager.WispesCollected -= price;
            GameManager.SaveGame();
            PurchaseState = PurchaseState.Success;
        }
        else
        {
            PurchaseState = PurchaseState.NotEnough;
        }
        OnAttemptedAPurchase(PurchaseState);
        return PurchaseState;
    }
}
