using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlatform : MonoBehaviour {

    public float fadeSpeed = 1f;
    public GameObject mesh;
    public bool isFadeing;

    public SpriteRenderer[] sprites;
    private bool fadeIn;
    private bool fadeOut;

    private void Start()
    {
        InitSprites();
        foreach (var sprite in sprites)
        {
            sprite.color = Color.clear;
        }
    }

    void InitSprites ()
    {
        int index = 0;
        for (int i = 0; i < mesh.transform.childCount; i++)
        {
            sprites[i] = mesh.transform.GetChild(i).GetComponent<SpriteRenderer>();
            index++;
        }
    }

    private void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        if (fadeOut)
        {
            FadeOut();
        }
    }

    public void StartFadeIn(float time)
    {
        /*if (fadeIn || fadeOut)
        {
            return;
        }*/
        StartCoroutine(DelayFadeIn(0));
    }

    public void StartFadeOut(float time)
    {
        /*if (fadeIn || fadeOut)
        {
            return;
        }*/
        StartCoroutine(DelayFadeOut(0));
    }

    private void FadeIn()
    {
        foreach (var sprite in sprites)
        {
            if (sprite.color.a >= 0.01f)
            {
                float alpha = Mathf.Lerp(sprite.color.a, 0, fadeSpeed * Time.deltaTime);
                Color desiredColor = new Color(1, 1, 1, alpha);
                
                sprite.color = desiredColor;
            }
            else
            {
                foreach (var s in sprites)
                {
                    s.color = new Color(1, 1, 1, 0);
                }
                fadeIn = false;
            }
        }
    }

    private void FadeOut()
    {
        foreach (var sprite in sprites)
        {
            if (sprite.color.a <= 0.95f)
            {
                float alpha = Mathf.Lerp(sprite.color.a, 1, fadeSpeed * Time.deltaTime);
                Color desiredColor = new Color(1, 1, 1, alpha);

                sprite.color = desiredColor;
            }
            else
            {
                foreach (var s in sprites)
                {
                    s.color = new Color(1, 1, 1, 1);
                }
                fadeOut = false;
            }
        }
    }

    IEnumerator DelayFadeOut(float fadeInTime)
    {
        yield return new WaitForSeconds(fadeInTime);
        fadeOut = true;
    }

    IEnumerator DelayFadeIn(float fadeInTime)
    {
        yield return new WaitForSeconds(fadeInTime);
        fadeIn = true;
    }
}
