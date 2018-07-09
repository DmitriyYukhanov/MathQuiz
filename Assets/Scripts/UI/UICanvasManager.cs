using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class UICanvasManager : MonoBehaviour
{
    //private MenuUI menuUI;
    private BattleUI battleUI;    

    // Use this for initialization
    void OnEnable() {

        //menuUI = UIController.Instance.CreateUI<MenuUI>("UI/MenuUI", UIStyle.POPUP);
        //menuUI.Open();

        //battleUI = UIController.Instance.CreateUI<BattleUI>("UI/BattleUI", UIStyle.UI);
        //battleUI.Init();
        //battleUI.Open();
        //battleUI.StartGame();

    }

    // test update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIAbstract uiwindow = UIController.Instance.PopWindow();
            if (uiwindow != null)
                uiwindow.Close();
        }
            
    }

}
