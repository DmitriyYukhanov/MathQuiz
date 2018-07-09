using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIBackState
{
    DO_NOT_BACK,
    ENABLE_BACK,
}

public class UIController
{
    // singleton
    private static UIController _instance = new UIController();
    public static UIController Instance { get { return _instance; } }


    // all window stack
    private Stack<UIAbstract> _uiwindowstack = new Stack<UIAbstract>();

    // currentUI
    private UIAbstract _currentUI;

    // back state
    private UIBackState _uiBackState = UIBackState.ENABLE_BACK;

    // canvas trans
    private Transform _canvasTransPanel;
    protected Transform canvasTransPanel
    {
        get
        {
            if(_canvasTransPanel == null)
            {
                GameObject panelgameObject = GameObject.Find("UICanvasManager/Panel");
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
        if(_uiwindowstack.Contains(subwindow) == false)
            _uiwindowstack.Push(subwindow);
    }

    /// <summary>
    /// Get Current UI TOP
    /// </summary>
    /// <returns></returns>
    public UIAbstract CurrentUI()
    {
        return _currentUI;
    }

    /// <summary>
    /// pop window
    /// </summary>
    /// <returns></returns>
    public UIAbstract GetBackWindow()
    {
        if (_uiBackState == UIBackState.DO_NOT_BACK)
            return null;
        
        if(_uiwindowstack.Count > 1)
        {
            UIAbstract backUI = _uiwindowstack.Pop();
            _currentUI = _uiwindowstack.Pop();
            if (_uiwindowstack.Count == 0)
                _uiwindowstack.Push(_currentUI);

            return backUI;
        }
        else
            return null;
    }

    /// <summary>
    /// Create UI
    /// </summary>    
    public T CreateUI<T>(string path, UIStyle uiStyle, UIAbstract opendMe = null, Transform parentTrans = null)
    {
        GameObject resGo = GameObject.Instantiate(Resources.Load(path) as GameObject);
        if (resGo != null)
        {            
            resGo.name = path.Remove(0, path.LastIndexOf('/') + 1);
            resGo.SetActive(false);
            if( parentTrans != null)
                resGo.transform.parent = parentTrans;
            else
                resGo.transform.parent = canvasTransPanel;

            resGo.transform.localPosition = Vector3.zero;
            resGo.transform.localScale = Vector3.one;
            T component = resGo.GetComponent<T>();

            _currentUI = component as UIAbstract;
            _currentUI.SetStyle(uiStyle, opendMe);

            if (component != null)
                return component;

            return default(T); 
        }

        return default(T);
    }

   
    
}

