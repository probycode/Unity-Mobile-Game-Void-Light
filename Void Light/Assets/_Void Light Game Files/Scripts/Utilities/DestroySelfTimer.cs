using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfTimer : MonoBehaviour {

    public float time;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DelayDestroy());
	}
	
	IEnumerator DelayDestroy ()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
