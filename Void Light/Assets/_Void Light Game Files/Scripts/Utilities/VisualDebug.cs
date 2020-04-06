using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualDebug : MonoBehaviour {

    public static VisualDebug Instance;
    public Text messageTextUI;

    private void Awake()
    {
        Instance = this;
    }

    public static void Debug<T> (T message)
    {
        Instance.messageTextUI.text = message.ToString();
    }
}
