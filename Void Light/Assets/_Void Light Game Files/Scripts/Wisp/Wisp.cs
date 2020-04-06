using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    public ColorPaletData colorPaletData;
    public Color color;
    public Transform target;
    public float moveSpeed = 4f;
    public AudioClip collectedSFX;
    private AudioSource _AudioSource;
    private bool isCollected = false;

    public delegate void WispCollectedHandler();
    public static event WispCollectedHandler WispCollected;

    public static void OnWispCollected()
    {
        if (WispCollected != null)
        {
            WispCollected();
        }
    }

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        //Optimize ME:
        target = GameObject.FindGameObjectWithTag("WispArea").transform;
        //SetRandomColor();
    }

    void SetRandomColor()
    {
        int ranColor = Random.Range(0, colorPaletData.GetColors().Length);
        color = GetComponentInChildren<SpriteRenderer>().color = colorPaletData.GetColors()[ranColor];
    }

    public Color GetColor ()
    {
        return color;
    }

    private void Update()
    {
        if (isCollected)
        {
            Animimate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isCollected = true;
        }
    }

    public void Animimate ()
    {
        Vector2 desiredPos = target.position;

        transform.position = Vector2.MoveTowards(transform.position, desiredPos, moveSpeed * Time.deltaTime);

        if(transform.position == target.position)
        {
            WispSFXController.Instance.PlaySFX(collectedSFX);
            OnWispCollected();
            Destroy(gameObject);
        }
    }
}
