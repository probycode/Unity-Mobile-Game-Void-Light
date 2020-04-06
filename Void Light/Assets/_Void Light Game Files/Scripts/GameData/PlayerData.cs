using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int highscore;
    public int score;
    public int wispsCollected;
    public int proggress;
    public bool isMusicOn = true;
    public bool isSFXOn = true;
    public int loopIndex;
    public int tillAdCounter;
    public bool initailizedData;
    public ItemManagerData itemManagerData = new ItemManagerData();
    public PlayerGameData playerGameData = new PlayerGameData();

    //
    // Summary:
    //     ///
    //     Type Float b/c cant Serialize Type Color
    //     ///
    public float[] baseColor = Utilities.ColorToFloatArray(Color.white);
    //
    // Summary:
    //     ///
    //     Type Float b/c cant Serialize Type Color
    //     ///
    public float[] auraColor = Utilities.ColorToFloatArray(Color.white);

    public PlayerData ()
    {

    }

    public PlayerData(PlayerData playerData)
    {
        highscore = playerData.highscore;
        score = playerData.score;
        proggress = playerData.proggress;
        isMusicOn = playerData.isMusicOn;
        isSFXOn = playerData.isSFXOn;
        loopIndex = playerData.loopIndex;
        baseColor = playerData.baseColor;
        auraColor = playerData.auraColor;
        wispsCollected = playerData.wispsCollected;
        itemManagerData = playerData.itemManagerData;
        initailizedData = playerData.initailizedData;
        tillAdCounter = playerData.tillAdCounter;
        playerGameData = playerData.playerGameData;
    }
}
