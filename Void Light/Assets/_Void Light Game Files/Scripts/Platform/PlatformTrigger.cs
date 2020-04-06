using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

    //put this in Platform class
    public GameObject playerSitPos;

    // Use this for initialization
    void Start ()
    {
        PlayerController.PlayerDead += GameManager_PlayerDead;
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDead -= GameManager_PlayerDead;
    }

    private void GameManager_PlayerDead()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
