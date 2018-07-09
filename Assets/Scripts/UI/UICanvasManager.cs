using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class UICanvasManager : MonoBehaviour
{
    private MenuUI menuUI;
    

    // Use this for initialization
    void OnEnable() {

        menuUI = UIController.Instance.CreateUI<MenuUI>("UI/MenuUI", UIStyle.UI);
        menuUI.Open();
    }

    // test update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 배버튼이 작동 안하도록 하는 상황 만들기
            UIAbstract uiwindow = UIController.Instance.GetBackWindow();
            if (uiwindow != null)
                uiwindow.Close();
        }
            
    }

}
