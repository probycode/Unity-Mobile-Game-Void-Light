using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStuff : MonoBehaviour {

    public bool isEnabled = true;
    public int itemToUnlock;
    public int addWispsAmount;

    private void Awake()
    {
        #if (UNITY_IOS || UNITY_ANDROID)
        //Destroy(gameObject);
        print("Uncomment Me ^");
        #endif
    }

    void Start ()
    {
        if (isEnabled == false)
        {
            return;
        }

        ScoreManager.WispesCollected = 1000;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isEnabled == false)
        {
            return;
        }
    }


    public void PlayerTestMode ()
    {
        if(isEnabled == false)
        {
            return;
        }

        if(GameManager.playerGO.testMode)
        {
            GameManager.playerGO.testMode = false;
        }
        else
        {
            GameManager.playerGO.testMode = true;
        }
    }
}
