using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CodexType
{
    TouchStone,
    DaAsRune,
    VeJelRune,
    ShiArRune,
    RaEnRune,
    UriEl
}

public class CodexUIController : ViewUI {

    public delegate void CodexUIEventHandler(CodexType codexType);
    public delegate void AllCodexViewedEventHandler();

    public static event CodexUIEventHandler CodexUIOpen;
    public static event AllCodexViewedEventHandler AllCodexViewed;

    //UI
    public CodexType codexType;
    public Text title_TEXT_UI;
    public Text summary_TEXT_UI;
    public Image rune_IMG_UI;
    //
    public float delayTextWriteTime;

    private int charIndex;
    private char[] characters;
    private CodexData codexData;

    public CodexData CodexData
    {
        get
        {
            return codexData;
        }

        set
        {
            codexData = value;
            OnUpdateUI();
            InitSummaryCharList();
            StartCoroutine(DelayText());
        }
    }

    public static void OnCodexUIOpen(CodexType codexType)
    {
        if (CodexUIOpen != null)
        {
            CodexUIOpen(codexType);
        }
    }

    public static void OnAllCodexViewed()
    {
        if (AllCodexViewed != null)
        {
            AllCodexViewed();
        }
    }

    public override void OnUpdateUI()
    {
        if(codexData == null)
        {
            return;
        }
        OnCodexUIOpen(codexType);
        summary_TEXT_UI.text = "";
        title_TEXT_UI.text = CodexData.title;
        rune_IMG_UI.sprite = CodexData.runeImage;
        summary_TEXT_UI.fontSize = CodexData.fontSize;
        codexType = codexData.codexType;

        rune_IMG_UI.SetNativeSize();

        if (codexType != CodexType.TouchStone)
        {
            RectTransform myRectTransform = title_TEXT_UI.GetComponent<RectTransform>();
            title_TEXT_UI.rectTransform.localPosition = new Vector3(0, myRectTransform.localPosition.y, 0);
        }
        else
        {
            //title_TEXT_UI.rectTransform.localPosition = initPos.position;
        }
    }

    void InitSummaryCharList ()
    {
        charIndex = 0;
        //characters = new char[summary.ToCharArray().Length];
        for (int i = 0; i < codexData.summary.ToCharArray().Length; i++)
        {
            characters = codexData.summary.ToCharArray();
        }
    }

    IEnumerator DelayText()
    {
        yield return new WaitForSeconds(delayTextWriteTime);
        if (charIndex < characters.Length)
        { 
        summary_TEXT_UI.text += characters[charIndex];
        charIndex++;
        StartCoroutine(DelayText());
        }
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void ButtonActionInput(UIButtonsActions UIButtonsActions)
    {
        
    }
}
