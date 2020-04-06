using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : SFXController{

    public static PlayerSFXController Instance;

    private void Awake()
    {
        Instance = this;
        _AudioSource = GetComponent<AudioSource>();
    }
}
