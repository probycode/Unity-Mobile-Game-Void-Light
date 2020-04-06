using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToLaunchEffect : MonoBehaviour {

    public float speed;
    public GameObject GhostPlayer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(gameObject);
        }
    }

}
