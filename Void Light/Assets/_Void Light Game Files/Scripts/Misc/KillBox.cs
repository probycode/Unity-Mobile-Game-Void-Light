using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

    public string playerTag = "Player";

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Death();
        }
    }
}
