using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextEffect : MonoBehaviour {

    public float cycleSpeed = 0.5f;
    public Sprite[] sprites;
    private Image _imageRenderer;
    private int indexSprite;

	// Use this for initialization
	void Start ()
    {
        _imageRenderer = GetComponent<Image>();
        StartCoroutine(CyclethroughSprites());
	}
	
	IEnumerator CyclethroughSprites ()
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleSpeed);
            _imageRenderer.sprite = sprites[indexSprite];
            _imageRenderer.SetNativeSize();
            if (indexSprite == sprites.Length - 1)
            {
                indexSprite = 0;
            }
            else
            {
                indexSprite++;
            }
            yield return null;
        }
    }
}
