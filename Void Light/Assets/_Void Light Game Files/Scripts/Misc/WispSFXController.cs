using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSFXController : SFXController {

    public static WispSFXController Instance;

    private void Awake()
    {
        Instance = this;
        _AudioSource = GetComponent<AudioSource>();
    }
}
