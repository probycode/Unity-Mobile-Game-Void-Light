using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEffect : MonoBehaviour {

    public ColorMode colorMode;
    public Transform target;
    public float moveSpeed = 4f;
    private bool trigger = false;

    private void Awake()
    {
        //Optimize ME:
        target = GameObject.FindGameObjectWithTag("WispArea").transform;
        CustomizePlayerViewController.ChangedColor += CustomizePlayerViewController_ChangedColor;
    }

    private void CustomizePlayerViewController_ChangedColor()
    {
        if(CustomizePlayerViewController.Instance.ColorMode == colorMode)
        {
            trigger = true;
        }
    }

    private void Update()
    {
        if (trigger)
        {
            Animimate();
        }
    }

    public void Animimate()
    {
        Vector2 desiredPos = target.position;

        transform.position = Vector2.MoveTowards(transform.position, desiredPos, moveSpeed * Time.deltaTime);

        if (transform.position == target.position)
        {
            //WispSFXController.Instance.PlaySFX(collectedSFX);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        CustomizePlayerViewController.ChangedColor -= CustomizePlayerViewController_ChangedColor;
    }
}
