using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WispColorFX : MonoBehaviour {

    public int id;
    public float speed = 0.5f;
    public float timeTillForceDone = 1;
    public Transform slot;
    public AudioClip sfx;
    public float volume = 0.2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        UnlockColorUIController.Reset += UnlockColorUIController_Reset;
    }

    private void UnlockColorUIController_Reset()
    {
        ResetObj();
    }

    public IEnumerator InitAndStart (Color color, Action OnComplete)
    {
        float timer = 0;
        GetComponentInChildren<Image>().color = color;
        while (transform.position != slot.position && timer <= timeTillForceDone)
        {
            transform.position = Vector3.MoveTowards(transform.position, slot.position, speed * 10 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SFXManager.PlayOneShot(sfx, volume));
        OnComplete();
    }

    private void OnDisable()
    {
        ResetObj();
    }

    public void ResetObj()
    {
        transform.position = startPos;
    }

    private void OnDestroy()
    {
        UnlockColorUIController.Reset -= UnlockColorUIController_Reset;
    }
}
