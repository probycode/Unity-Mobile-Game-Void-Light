using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingFX : MonoBehaviour {

    
    [Range(0,3)]
    public float speed;
    public float height = 0.5f;
    public Vector3 offeset;

    private void Start()
    {
        transform.position += offeset;
    }

    void Update ()
    {
        float y = Mathf.Sin(speed * Time.time);
        transform.position += new Vector3(0,y * height,0);
	}
}
