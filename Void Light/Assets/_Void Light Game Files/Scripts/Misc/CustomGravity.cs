using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour {

    [SerializeField]
    public static Vector3 GlobelGravity;

    public Vector2 forceDown;


    private Rigidbody2D m_Rigid;

    // Use this for initialization
    void Start () {
        m_Rigid = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Gravity();
    }

    void Gravity()
    {
        m_Rigid.AddForce(forceDown, ForceMode2D.Force);
    }
}
