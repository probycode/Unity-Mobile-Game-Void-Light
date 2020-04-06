using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities  {

    public static float[] ColorToFloatArray(Color color)
    {
        float[] colorConvered = new float[4];
        colorConvered[0] = color.r;
        colorConvered[1] = color.g;
        colorConvered[2] = color.b;
        colorConvered[3] = color.a;

        return colorConvered;
    }

    public static Color FloatToColor(float[] colorAsFloat)
    {
        Color colorConvered = new Color();
        colorConvered.r = colorAsFloat[0];
        colorConvered.g = colorAsFloat[1];
        colorConvered.b = colorAsFloat[2];
        colorConvered.a = colorAsFloat[3];

        return colorConvered;
    }
}
