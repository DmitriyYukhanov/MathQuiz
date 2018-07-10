using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class UICanvasManager : MonoBehaviour
{
    // menuUI
    private SelectQuizMenuUI selectQuizMenuUI;
    

    // Use this for initialization
    void OnEnable() {

        selectQuizMenuUI = UIController.Instance.CreateUI<SelectQuizMenuUI>("UI/SelectQuizMenuUI", UIStyle.UI);
        selectQuizMenuUI.Open();
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
