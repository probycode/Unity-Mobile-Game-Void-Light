using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCell : CellUI
{
    public Color colorTemp;
    public Sprite blackCell;
    private ColorItem colorItem;
    private Button m_Button;
    private CustomizePlayerViewController customizePlayerView;
    private Image m_Image_Color;

    public ColorItem ColorItem
    {
        get
        {
            return colorItem;
        }

        set
        {
            colorItem = value;
            InitButtonGraphics();
        }
    }

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Image_Color = GetComponent<Image>();

        InitSubEvents();
        InitButton();
        GetComponent<AudioButtonUI>().Mute();
    }

    private void InitSubEvents()
    {
        ItemManager.ItemUnlocked += ItemManager_ItemUnlocked;
    }

    private void ItemManager_ItemUnlocked(Item item)
    {
        GetComponent<AudioButtonUI>().UnMute();
        //print(item.id + " : " + colorItem.id);
        if (item.id == colorItem.id)
        {
            if (item.isLocked == false)
            {
                if (colorItem.id == 4)
                {
                    IfBlackColorCell();
                    return;
                }
                else
                {
                    ColorItem colorCell = item as ColorItem;
                    m_Image_Color.color = colorTemp;
                }
            }
            else
            {
                ColorItem colorCell = item as ColorItem;
                m_Image_Color.color = Color.black;
            }
        }
    }

    private void InitButtonGraphics()
    {
        colorTemp = Utilities.FloatToColor(colorItem.color);

        if (colorItem.isLocked)
        {
            return;
        }

        if (colorItem.id == 4)
        {
            IfBlackColorCell();
            return;
        }

        m_Image_Color.color = Utilities.FloatToColor(colorItem.color);
    }

    public void IfBlackColorCell()
    {
        m_Image_Color.sprite = blackCell;
        transform.localScale = new Vector3(1.066046f, 1.066046f, 1.066046f);
        Color c = new Color(0.6f, 0.6f, 0.6f);
        m_Image_Color.color = c;
    }

    public void InitButton()
    {
        m_Button.onClick.AddListener(() => Clicked());
    }

    public void Clicked()
    {
        if (colorItem.isLocked == true)
        {
            return;
        }

        GetComponent<AudioButtonUI>().UnMute();

        if (CustomizePlayerViewController.Instance.ColorMode == ColorMode.AuraColor)
        {
            GameManager.playerData.auraColor = colorItem.color;
            GameManager.SaveGame();
            //print("Change base to " + Utilities.FloatToColor(Utilities.ColorToFloatArray(color)));
            CustomizePlayerViewController.OnChangedColor();
        }
        else if (CustomizePlayerViewController.Instance.ColorMode == ColorMode.baseColor)
        {
            GameManager.playerData.baseColor = colorItem.color;
            GameManager.SaveGame();
            //print("Change base to " + Utilities.FloatToColor(Utilities.ColorToFloatArray(color)));
            CustomizePlayerViewController.OnChangedColor();
        }
    }

    private void OnDestroy()
    {
        ItemManager.ItemUnlocked -= ItemManager_ItemUnlocked;
    }

}
