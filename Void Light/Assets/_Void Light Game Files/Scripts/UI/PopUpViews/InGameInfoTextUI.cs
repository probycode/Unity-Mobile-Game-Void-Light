using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameInfoTextUI : MonoBehaviour {

    public AudioClip sfx;
    public float volume;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        Platform.ReachedLastPlat += Platform_ReachedLastPlat;
        PlayerController.PlayerReachedStartPlat += PlayerController_PlayerReachedStartPlat;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && image.enabled == true)
        {
            image.enabled = false;
        }
    }

    private void PlayerController_PlayerReachedStartPlat()
    {
        image.enabled = false;
    }

    private void Platform_ReachedLastPlat(GameObject platform)
    {
        ViewController.PlaySound(sfx,false,volume);
        image.enabled = true;
    }

    private void OnDestroy()
    {
        Platform.ReachedLastPlat -= Platform_ReachedLastPlat;
        PlayerController.PlayerReachedStartPlat -= PlayerController_PlayerReachedStartPlat;
    }
}
