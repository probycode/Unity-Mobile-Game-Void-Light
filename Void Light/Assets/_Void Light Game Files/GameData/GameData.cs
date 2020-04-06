using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerGameData
{
    public Dictionary<CodexType, bool> codexViewedList;
}

[Serializable]
public static class GameData {

    public static Dictionary<CodexType,bool> codexViewedList = new Dictionary<CodexType, bool>();
	
    public static void Init ()
    {
        InitSubEvents();
    }

    private static void InitSubEvents ()
    {
        CodexUIController.CodexUIOpen += CodexUIController_CodexUIOpen;
    }

    private static void CodexUIController_CodexUIOpen(CodexType codexType)
    {
        if (codexViewedList.ContainsKey(codexType))
        {
            /*if(codexViewedList[codexType] == false)
            {
                codexViewedList[codexType] = true;
                Debug.Log("Viewed : " + codexType);
            }*/
        }
        else
        {
            codexViewedList.Add(codexType, true);
        }
        if(IsAllCodexViewed())
        {
            CodexUIController.OnAllCodexViewed();
        }
    }

    public static bool IsAllCodexViewed ()
    {
        if(codexViewedList.Count == 6)
        {
            Debug.Log("IsAllCodexViewed true");
            return true;
        }
        else
        {
            Debug.Log("IsAllCodexViewed false");
            return false;
        }
    }

    public static void LoadData(PlayerGameData playerGameData)
    {
        codexViewedList = playerGameData.codexViewedList;
    }

    public static PlayerGameData GetData()
    {
        PlayerGameData playerGameData;

        playerGameData.codexViewedList = codexViewedList;

        return playerGameData;
    }
}
