using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : UIAbstract
{

    /// <summary>
    /// 버튼
    /// </summary>
    private Transform[] buttons;

    /// <summary>
    /// tween
    /// </summary>
    private EasyTween[] uiButtonEventTween;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OpenUI()
    {
        base.OpenUI();
    }

}
