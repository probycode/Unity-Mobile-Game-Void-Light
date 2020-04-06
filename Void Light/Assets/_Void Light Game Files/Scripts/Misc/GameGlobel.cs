using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobel : MonoBehaviour {

    public const int SKIP_AD_PRICE = 15;
    public const int AMOUNT_OF_COLORS_TO_UNLOCK = 3;
    public const int BUY_COLOR_PRICE = 15;
    public const int WATCH_AD_EARN_AMOUNT = 10;
    public const string DEFAULT_LAYER = "Default";
    public const string DARKEFFECT_LAYER = "DarkEffect";

    public static GameGlobel Instance;
    public Sprite wispRuneSprite;

    private void Awake()
    {
        Instance = this;
    }
}
