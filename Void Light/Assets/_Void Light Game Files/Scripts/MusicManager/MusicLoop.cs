using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour {

    Animator loopAnim;
    public bool isLoopActive;

    void Awake ()
    {
        MusicManager.MusicStatChanged += MusicManager_MusicStatChanged;
        loopAnim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        MusicManager.MusicStatChanged -= MusicManager_MusicStatChanged;
    }

    public void TransitionUp()
    {
        if (loopAnim = GetComponent<Animator>())
        {
            loopAnim.SetTrigger("TransitionUp");
            isLoopActive = true;
        }
        else
        {
            Debug.Log(loopAnim);
        }
    }

    public void TransitionDown()
    {
        if (loopAnim = GetComponent<Animator>())
        {
            loopAnim.SetTrigger("TransitionDown");
            isLoopActive = false;
        }
        else
        {
            Debug.Log(loopAnim);
        }
    }

    private void MusicManager_MusicStatChanged()
    {
        
    }
}
