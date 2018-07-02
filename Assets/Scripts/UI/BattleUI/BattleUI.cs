using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class BattleUI : UIAbstract
{
    
    // quiz label center label tween
    [SerializeField]
    private EasyTween[] tweenQuizLabel;

    // answer Label tween
    [SerializeField]
    private EasyTween[] tweenAnswerLabel;

    // QuestionLabel
    [SerializeField]
    private UnityEngine.UI.Text questionLabel;

    // AnsweLabel
    [SerializeField]
    private UnityEngine.UI.Text[] answerLabels;
    
    // quiz data and information
    private MakeQuestion quiz;

    // button delay
    private bool IsButtonActive = false;

    // start animation time
    private ObscuredFloat startTime = 0.0f;

    //
    private ObscuredBool IsForceFail = false;
    
    public void Init()
    {
        quiz = new MakeQuestion(QuestionType.ADD, 10);
        for (int i = 0; i < tweenAnswerLabel.Length; i++)
        {
            startTime = tweenAnswerLabel[i].GetAnimationDuration();
            UnityEngine.UI.Button buton = tweenAnswerLabel[i].gameObject.GetComponent<UnityEngine.UI.Button>();

            string index = i.ToString();
            buton.onClick.RemoveAllListeners();
            buton.onClick.AddListener(delegate 
            {
                OnPressedEvent(index);
            });
        }
    }

    public void Clear()
    {
        quiz.Clear();
    }

    public void StartGame()
    {
        Invoke("NextQuiz", startTime + 0.1f);        
    }

    private bool NextQuiz()
    {
        if (quiz.SetNextQuestion(questionLabel, answerLabels) == false)
            return false;

        StartAnimation();
        return true;
    }

    // start animation
    private void StartAnimation()
    {
        tweenQuizLabel[0].SetStartValues();
        tweenQuizLabel[0].ChangeSetState(false);
        tweenQuizLabel[0].OpenCloseObjectAnimation();
        float buttonActiveTime = tweenQuizLabel[0].GetAnimationDuration();

        for (int i = 0; i < tweenAnswerLabel.Length; i++)
        {
            tweenAnswerLabel[i].SetStartValues();
            tweenAnswerLabel[i].ChangeSetState(false);
            tweenAnswerLabel[i].OpenCloseObjectAnimation();
            tweenAnswerLabel[i].GetAnimationDuration();
        }

        Invoke("SetButtonActive", buttonActiveTime + 0.1f);
    }

    // button Event
    private void OnPressedEvent(string data)
    {
        if (IsButtonActive == false)
            return;

        IsButtonActive = false;

        int index = int.Parse(data);
        bool IsCorrect = quiz.CorrectAnswer(index);

        tweenQuizLabel[1].SetStartValues();
        tweenQuizLabel[1].ChangeSetState(false);
        tweenQuizLabel[1].OpenCloseObjectAnimation();
        float NextTime = tweenQuizLabel[1].GetAnimationDuration();
        Invoke("NextQuiz", NextTime + 0.1f);

        if (IsCorrect)
        {
            
        }
        else
        {
            
        }
    }
    
    // button Event Active
    private void SetButtonActive()
    {
        IsButtonActive = true;

        // start time 
    }

    // onApp Pause don't cheat.
    void OnApplicationPause(bool pauseStatus)
    {
        IsForceFail = true;
    }
}
