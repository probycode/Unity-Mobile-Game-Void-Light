using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXController : MonoBehaviour {

    protected AudioSource _AudioSource;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    virtual public void PlaySFX(AudioClip clip)
    {
        _AudioSource.PlayOneShot(clip);
    }
}
