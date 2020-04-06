using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ViewUIButton : MonoBehaviour {

    protected Button btn;

    abstract protected void ButtonAction();

    private void Awake()
    {
        InitBtn();
    }

    virtual protected void InitBtn()
    {
        if (GetComponent<Button>() == false)
        {
            print("Obj " + name + " Has no Button component");
            return;
        }
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => ButtonAction());
    }
}
