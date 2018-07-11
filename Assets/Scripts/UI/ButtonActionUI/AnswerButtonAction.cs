using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButtonAction : MonoBehaviour {
        
    // Answer button bg gameObject
    [SerializeField]
    private GameObject tweenBgGo;
    
    public void OnTurnOnBackGround()
    {
        tweenBgGo.SetActive(true);
    }

    public void OnTurnOffBackGround()
    {
        tweenBgGo.SetActive(false);
    }
}
