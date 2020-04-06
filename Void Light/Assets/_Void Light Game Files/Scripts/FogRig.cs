using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogRig : MonoBehaviour {

    public float rotateSpeed;

    //private Transform _initTransform;

    private void Awake()
    {
        //_initTransform = transform;
        ViewController.ViewChanged += ViewController_ViewChanged;
        ViewController.InitiatingViewChange += ViewController_InitiatingViewChange;
    }

    private void ViewController_InitiatingViewChange(View destiniationView)
    {
        if(destiniationView != View.CustomizePlayer || destiniationView != View.Options)
        {
            ResetObject();
        }
    }

    private void ViewController_ViewChanged(View view)
    {
        if(view == View.CustomizePlayer || view == View.Options || view == View.Title || view == View.ColorUnlock || view == View.InGame)
        {
            StartCoroutine(RotateObject());
        }
    }

    public IEnumerator RotateObject()
    {
        while(true)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            yield return null;
        }
    }

    public void ResetObject()
    {
        StopAllCoroutines();
        transform.eulerAngles = Vector3.zero;
    }
}
