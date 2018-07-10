using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Closure 
public struct QuetionTypeClosure
{
    public QuestionType questionType;
}

public class SelectQuizMenuUI : UIAbstract
{
    // button tweens
    [SerializeField]
    private EasyTween[] uiButtonEventTween;

    private BattleUI battleUI;

    // Use this for initialization
    void Start()
    {
        if (uiButtonEventTween == null)
        {
            uiButtonEventTween = GetComponentsInChildren<EasyTween>();
            foreach (EasyTween tween in uiButtonEventTween)
                tween.gameObject.SetActive(false);
        }
    }

    // enable active on
    private void OnEnable()
    {
        Invoke("InvokeShowButton", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // show button
    private void InvokeShowButton()
    {        
        int index = 0;
        foreach (EasyTween tween in uiButtonEventTween)
        {
            tween.gameObject.SetActive(true);
            tween.ResetStartAction();

            QuetionTypeClosure eventIndex;
            eventIndex.questionType = (QuestionType)index++;
            // index 를 사용하게되면 stack 저장되어 값을 공유하게 됨. 
            // 고정값을 가질 수 없게됨.
            UnityEngine.UI.Button buttonevent = tween.gameObject.GetComponent<UnityEngine.UI.Button>();
            buttonevent.onClick.RemoveAllListeners();
            buttonevent.onClick.AddListener(delegate
            {
                OnButtonEvent(eventIndex.questionType);
            });
        }
    }

    // button event
    private void OnButtonEvent(QuestionType quetionType)
    {
        if (battleUI == null)
        {
            battleUI = UIController.Instance.CreateUI<BattleUI>("UI/BattleUI", UIStyle.UI, this);            
        }

        Close();
        battleUI.Init(quetionType);
        battleUI.Open();
        battleUI.StartGame();
    }
}
