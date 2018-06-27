using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : UIAbstract
{
    // quiz label center label
    [SerializeField]
    private EasyTween[] tweenQuizLabel;

    
    private int tweenQuizIndex = 0;

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            tweenQuizLabel[tweenQuizIndex].SetStartValues();
            tweenQuizLabel[tweenQuizIndex].ChangeSetState(false);
            tweenQuizLabel[tweenQuizIndex].OpenCloseObjectAnimation();                        
            tweenQuizLabel[tweenQuizIndex].GetAnimationDuration();
            tweenQuizIndex++;
            if (tweenQuizIndex >= tweenQuizLabel.Length)
                tweenQuizIndex = 0;
        }        
    }  
}
