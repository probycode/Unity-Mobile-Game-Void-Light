using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour {

    public GameObject effectPrefab;
    public Vector3 offset;
    public bool isEnabled;

    private void Awake()
    {
        if (isEnabled)
        {
            InitSubEvents();
        }
    }

    private void InitSubEvents ()
    {
        CustomizePlayerViewController.ChangedColor += CustomizePlayerViewController_ChangedColor;
    }

    private void CustomizePlayerViewController_ChangedColor()
    {
        GameObject instance =  Instantiate(effectPrefab)as GameObject;
        instance.transform.position = transform.position + offset;
    }

    private void OnDestroy()
    {
        CustomizePlayerViewController.ChangedColor -= CustomizePlayerViewController_ChangedColor;
    }
}
