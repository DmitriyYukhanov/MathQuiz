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

    public delegate void OnDelegateEvent();
    public event OnDelegateEvent OnExitEventHandler;

    // open me parent UI
    protected UIAbstract parentOpenMeUI;

    // constructor
    public UIAbstract()
    {
        windowname = this.GetType().Name;
    }

    // setting style
    public void SetStyle(UIStyle style, UIAbstract opendMe)
    {   
        if(uiStype == UIStyle.NONE)
            uiStype = style;

        parentOpenMeUI = opendMe;
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

        if (OnExitEventHandler != null)
            OnExitEventHandler();

    }
}

