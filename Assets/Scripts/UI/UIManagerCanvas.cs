using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class UIManagerCanvas : MonoBehaviour
{
    private MenuUI menuUI;
    private BattleUI battleUI;    

    // Use this for initialization
    void OnEnable() {

        menuUI = UIController.Instance.CreateUI<MenuUI>("UI/MenuUI");
        menuUI.OpenUI();

        battleUI = UIController.Instance.CreateUI<BattleUI>("UI/BattleUI");
        battleUI.OpenUI();        
    }  

}
