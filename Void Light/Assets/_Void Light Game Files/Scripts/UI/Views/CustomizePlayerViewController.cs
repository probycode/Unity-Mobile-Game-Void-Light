using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ColorMode
{
    baseColor,
    AuraColor
}

public class CustomizePlayerViewController : ViewUI
{
    public delegate void ChangePlayerColorEventHandler();

    public static event ChangePlayerColorEventHandler ChangedColor;

    public static CustomizePlayerViewController Instance;

    public PointerUI pointerUI;
    public Text wispAmountUI;
    public Image auraImage;
    public Image baseImage;

    public GameObject playerPrefab;
    public GameObject colorButtonPrefab;
    public GameObject setPrefab;
    public Transform content;
    public Transform spawnPos;
    private ColorMode colorMode;
    public AudioClip mixColorSFX;
    public ColorPaletData[] colorPalets;

    //Make dict to connect Item with GO
    private Dictionary<Item,GameObject> colorItemsCellsGOs;
    private List<GameObject> setCellsGOs;
    private bool didCreatedColorCells;

    private GameObject playerInstence;

    public GameObject PlayerInstence
    {
        get
        {
            return playerInstence;
        }

        set
        {
            playerInstence = value;
        }
    }

    public ColorMode ColorMode
    {
        get
        {
            return colorMode;
        }

        set
        {
            colorMode = value;
        }
    }

    public static void OnChangedColor()
    {
        if (ChangedColor != null)
        {
            ChangedColor();
        }
    }

    private void Awake()
    {
        Instance = this;
        colorItemsCellsGOs = new Dictionary<Item, GameObject>();
        setCellsGOs = new List<GameObject>();
        InitSubEvents();
    }

    public new void InitSubEvents()
    {
        base.InitSubEvents();
        GameManager.GameLoaded += GameManager_GameLoaded;
        CustomizePlayerViewController.ChangedColor += CustomizePlayerViewController_ChangedColor;
    }

    private void GameManager_GameLoaded()
    {
        
    }

    private void CustomizePlayerViewController_ChangedColor()
    {
        Destroy(PlayerInstence.gameObject);
        CreatePlayer();
        baseImage.color = Utilities.FloatToColor(GameManager.playerData.baseColor);
        auraImage.color = Utilities.FloatToColor(GameManager.playerData.auraColor);
    }

    private void OnEnable()
    {
        CreatePlayer();
        if (didCreatedColorCells == false)
        {
            CreateColorButtonsCells();
            content.GetComponent<ScrollSnapRect>().enabled = true;
            didCreatedColorCells = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
        CustomizePlayerViewController.ChangedColor -= CustomizePlayerViewController_ChangedColor;
    }

    private void OnDisable()
    {
        CleanUp();
    }

    override public void OnUpdateUI()
    {
        wispAmountUI.text = ScoreManager.WispesCollected.ToString();

        baseImage.color = Utilities.FloatToColor(GameManager.playerData.baseColor);
        auraImage.color = Utilities.FloatToColor(GameManager.playerData.auraColor);
    }

    private void CreatePlayer()
    {
        PlayerInstence = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity) as GameObject;
        PlayerInstence.transform.localPosition = spawnPos.position;
        PlayerInstence.GetComponent<Animator>().SetTrigger("shopAnim");
        PlayerController playerController = PlayerInstence.GetComponent<PlayerController>();
        playerController.enabled = false;
    }

    private void CreateColorButtonsCells ()
    {
        int index = 0;
        for (int set = 0; set < colorPalets.Length; set++)
        {
            GameObject setInstance = Instantiate(setPrefab) as GameObject;
            setInstance.transform.SetParent(content.transform, false);
            setCellsGOs.Add(setInstance);

            for (int i = 0; i < 14; i++)
            {
                GameObject cell = Instantiate(colorButtonPrefab) as GameObject;
                cell.transform.SetParent(setInstance.transform, false);

                Color color = colorPalets[set].GetColors()[i];

                //Setup Data
                ColorItem colorItem = new ColorItem();
                //If we have an item already, then use saved version of that item
                if (ItemManager.ContainsItem(index))
                {
                    colorItem = ItemManager.GetItem(index) as ColorItem;
                }
                else
                {
                    colorItem.id = index;
                }
                colorItem.color = Utilities.ColorToFloatArray(color);

                //colorItemsCellsGOs.Add(colorItem, cell);

                ItemManager.AddItem(colorItem);

                cell.GetComponent<ColorCell>().ColorItem = colorItem;

                //Unlock First Color
                if (index <= 2)
                {
                    ItemManager.UnlockItem(colorItem);
                }

                index++;
            }
        }
        //GameManager.SaveGame();
    }

    public void CleanUp ()
    {
        Destroy(PlayerInstence.gameObject);
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
