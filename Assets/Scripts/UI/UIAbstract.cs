using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIStyle
{
    NONE,
    UI,
    POPUP,
}

public abstract class UIAbstract : MonoBehaviour
{
    // current window name
    protected string windowname = string.Empty;

    // ui style
    protected UIStyle uiStype = UIStyle.NONE;

    // open chek
    protected bool IsOpen = false;

    // constructor
    public UIAbstract()
    {
        windowname = this.GetType().Name;
    }

    // setting style
    public void SetStyle(UIStyle style)
    {
        if(uiStype == UIStyle.NONE)
            uiStype = style;
    }
    
    // open
    public virtual void Open()
    {
        if (IsOpen)
            return;

        IsOpen = true;            
        UIController.Instance.PushWindow(this);
        gameObject.SetActive(true);
    }

    // close
    public void Close()
    {
        IsOpen = false;
        gameObject.SetActive(false);
        switch (uiStype)
        {
            case UIStyle.POPUP:
                GameObject.Destroy(gameObject);
                break;
        }   
    }    
}

