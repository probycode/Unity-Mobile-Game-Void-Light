using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockColorUIController : ViewUI {

    public delegate void UnlockColorHandler();

    public static event UnlockColorHandler Reset;

    public Image textImage;
    public Button unlockButton;
    public GameObject chestOpened;
    public GameObject chestClosed;
    public AudioClip openChestSFX;
    public float volume = 0.2f;
    public WispColorFX[] wispColors;

    public static UnlockColorUIController Instance;
    public Text wispAmountUI;
    public int amountOfButtonSlots;
    private int amountOfButtonOff;

    public static void OnReset()
    {
        if (Reset != null)
        {
            Reset();
        }
    }

    private void Awake()
    {
        Instance = this;
        InitSubEvents();
    }

    private void Start()
    {
        HasAllColorTextCheck();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
        UnlockColorUIController.Reset += UnlockColorUIController_Reset;
        PurchaseManager.PurchaseInitiated += PurchaseManager_PurchaseInitiated;
    }

    private void UnlockColorUIController_Reset()
    {
        chestClosed.GetComponent<Mask>().enabled = false;
        StartCoroutine(chestOpened.GetComponent<ChestUI>().CloseAnim());
    }

    private void PurchaseManager_PurchaseInitiated(List<Item> Items)
    {
        isBusy = true;
        
        ViewController.PlaySound(openChestSFX, true , volume);

        Fading.Instance.BeginFade(1, 20f, Color.white, () =>
        {
            chestClosed.GetComponent<Mask>().enabled = true;
            chestOpened.GetComponent<Image>().color = new Color(1,1,1,1);

            int i = 0;
            if(Items[i] == null)
            {
                return;
            }
            ColorItem colorItem = Items[i] as ColorItem;
            Color color = Utilities.FloatToColor(colorItem.color);

            StartCoroutine(wispColors[i].InitAndStart(color, () =>
            {
                i++;
                AnimateWisp(i, Items);
            }));
        });
    }

    private void AnimateWisp (int i, List<Item> Items)
    {
        if(i >= Items.Count)
        {
            StartCoroutine(DoneAnimatingWisps());
            return;
        }

        ColorItem colorItem = Items[i] as ColorItem;
        Color color = Utilities.FloatToColor(colorItem.color);

        StartCoroutine(wispColors[i].InitAndStart(color, () =>
        {
            i++;
            AnimateWisp(i, Items);
        }));
    }

    IEnumerator DoneAnimatingWisps()
    {
        PurchaseManager.OnPurchaseFinished();
        yield return new WaitForSeconds(2f);
        OnReset();
        HasAllColorTextCheck();
        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }

    public void HasAllColorTextCheck ()
    {
        if(ItemManager.ItemID == 0)
        {
            return;
        }
        if (ItemManager.IsAllItemsUnlocked())
        {
            textImage.enabled = true;
        }
        else
        {
            textImage.enabled = false;
        }
    }

    override public void OnUpdateUI()
    {
        wispAmountUI.text = ScoreManager.WispesCollected.ToString();
    }

    private void OnEnable()
    {
        HasAllColorTextCheck();
    }

    private void OnDisable()
    {
        chestClosed.GetComponent<Mask>().enabled = false;
        chestOpened.GetComponent<Image>().color = new Color(1,1,1,0);
    }

    private void OnDestroy()
    {
        UnlockColorUIController.Reset -= UnlockColorUIController_Reset;
        PurchaseManager.PurchaseInitiated -= PurchaseManager_PurchaseInitiated;
    }

    override public void Close()
    {
        gameObject.SetActive(false);
    }

    override public void Open()
    {
        gameObject.SetActive(true);
    }

    public override void ButtonActionInput(UIButtonsActions UIButtonsActions)
    {
        throw new NotImplementedException();
    }
}
