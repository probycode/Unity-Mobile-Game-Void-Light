using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour {

    public float closeSpeed;

	public IEnumerator CloseAnim ()
    {
        Image graphics = GetComponent<Image>();
        while(graphics.color.a > 0)
        {
            graphics.color = new Color(1,1,1, graphics.color.a - (closeSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
