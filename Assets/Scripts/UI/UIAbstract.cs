using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAbstract : MonoBehaviour
{
    protected string windowname = string.Empty;
    
    public virtual void OpenUI()
    {
        windowname = this.GetType().Name;
        UIController.Instance.PushWindow(this);
    }

    public virtual UIAbstract CloseUI()
    {   
        return UIController.Instance.PopWindow();
    }
}

