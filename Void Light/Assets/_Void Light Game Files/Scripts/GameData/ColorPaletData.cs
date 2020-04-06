using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Color Palet")]
public class ColorPaletData : ScriptableObject {

    [SerializeField]
    Color[] colors;

    public Color[] GetColors()
    {
        return colors;
    }
}
