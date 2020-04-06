using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PointerUI : MonoBehaviour {

    public float moveSpeed = 0.5f;

    private Vector3 _desiredPosition;
    private BobbingFX bobFX;

    private void Awake()
    {
        bobFX = GetComponent<BobbingFX>();
    }

    public void SetPointer(Vector3 desiredPosition, Action OnComplete)
    {
        bobFX.enabled = false;
        StartCoroutine(SetPointerCorouten(desiredPosition, OnComplete));
    }

    private IEnumerator SetPointerCorouten(Vector3 desiredPosition, Action OnComplete)
    {
        if(_desiredPosition == desiredPosition)
        {
            yield return null;
        }
        while(transform.position != desiredPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * 1000 * Time.deltaTime);
            yield return null;
        }
        bobFX.enabled = true;
        OnComplete();
    }
}
