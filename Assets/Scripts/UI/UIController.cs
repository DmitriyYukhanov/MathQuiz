using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController
{
    private static UIController _instance = new UIController();
    public static UIController Instance
    {
        get {

            return _instance;
        }
    }
    

    private Stack<UIAbstract> _uiwindowstack = new Stack<UIAbstract>();


    private Transform _canvasTransPanel;
    protected Transform canvasTransPanel
    {
        get
        {
            if(_canvasTransPanel == null)
            {
                GameObject panelgameObject = GameObject.Find("UIManagerCanvas/Panel");
                if (panelgameObject != null)
                    _canvasTransPanel = panelgameObject.transform;
                else
                    Debug.Log("=== Error No Panel");
            }

            return _canvasTransPanel;
        }
    }


    /// <summary>
    ///  push window
    /// </summary>
    /// <param name="subwindow"></param>
    public void PushWindow(UIAbstract subwindow)
    {
        _uiwindowstack.Push(subwindow);
    }

    /// <summary>
    /// pop window
    /// </summary>
    /// <returns></returns>
    public UIAbstract PopWindow()
    {
        if (_uiwindowstack.Count > 0)
            return _uiwindowstack.Pop();
        else
            return null;
    }

    /// <summary>
    /// Create UI
    /// </summary>    
    public T CreateUI<T>(string path, Transform parentTrans = null)
    {
        GameObject resGo = GameObject.Instantiate(Resources.Load(path) as GameObject);
        if (resGo != null)
        {
            resGo.name = path;
            resGo.SetActive(false);
            if( parentTrans != null)
                resGo.transform.parent = parentTrans;
            else
                resGo.transform.parent = canvasTransPanel;

            resGo.transform.localPosition = Vector3.zero;
            resGo.transform.localScale = Vector3.one;
            T component = resGo.GetComponent<T>();
            if(component != null)
                return component;

            return default(T); 
        }

        return default(T);
    }
    
}

