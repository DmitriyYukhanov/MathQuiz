using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class BattleUI : UIAbstract
{

    //==========================================================
    // serializedField
    //==========================================================

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

    // point label
    [SerializeField]
    private UnityEngine.UI.Text pointLabel;

    // time lable
    [SerializeField]
    private UnityEngine.UI.Text secLabel;
    //==========================================================
    // variable
    //==========================================================

    // quiz data and information
    private MakeQuestion quiz;

    // button delay
    private bool IsButtonActive = false;

    // start animation time
    private ObscuredFloat startTime = 0.0f;

    // OnApp Pause Fail process
    private ObscuredBool IsForceFail = false;

    // time update flag
    private bool IsUpdatePoint = false;

    // time 
    private float updatePointTime = 0f;

    // speed
    private int updateSpeed = 1;

    // target update max point
    private int targetUpdateMaxPoint = 0;

    // update point
    private int updatePoint = 0;

    // fixed total point
    private float fixedUpdateTime = 0f;

    // current target point
    private int targetTotalPoint = 0;

    // Time delay
    private bool IsTimeDelay = false;

    private float limitTime;

    public void Init()
    {
        
        SetPoint(0);
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
        NextQuizAnimated();

        if (IsCorrect)
        {
            quiz.CalcuratePoint();         
        }
        else
        {
            quiz.CalcuratePoint();
        }
    }

    // next quize animated
    private void NextQuizAnimated()
    {
        tweenQuizLabel[1].SetStartValues();
        tweenQuizLabel[1].ChangeSetState(false);
        tweenQuizLabel[1].OpenCloseObjectAnimation();
        float NextTime = tweenQuizLabel[1].GetAnimationDuration();
        Invoke("NextQuiz", NextTime + 0.1f);

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

    private void SetPoint(int point)
    {
        pointLabel.text = string.Format("{0:#,###}", point);
    }

    private void SetTime(int time){
        secLabel.text = string.Format("{0}", time);
    }

    
    private void Update()
    {
        limitTime += Time.deltaTime;
        if (limitTime >= 1.0f)
        {
            limitTime = 0f;
            SetTime(quiz.UpdateDownTime);
        }

        if (IsUpdatePoint)
        {
            updatePointTime += Time.deltaTime;
            if (updatePointTime >= fixedUpdateTime)
            {
                updatePointTime = 0f;

                CheckTimeReduce(targetUpdateMaxPoint, updatePoint);

                updatePoint = Mathf.Min(updatePoint + updateSpeed, targetUpdateMaxPoint);

                if (updatePoint == targetUpdateMaxPoint)
                {                    
                    IsUpdatePoint = false;
                    targetTotalPoint += targetUpdateMaxPoint;
                    SetPoint(targetTotalPoint);
                    return;
                }
                                         
                SetPoint(targetTotalPoint + updatePoint);                
            }
            return;
        }

        if (quiz.IsEnablePoint && IsUpdatePoint == false)
        {
            fixedUpdateTime = 0.0001f;
            updatePoint = 0;
            IsUpdatePoint = true;
            IsTimeDelay = false;

            updateSpeed = 2100;

            targetUpdateMaxPoint = quiz.DequePoint();
        }

    }

    // time reduce
    private void CheckTimeReduce(int a, int b)
    {
        if (IsTimeDelay)
            return;

        if (a > b)
            if (a - b <= 2000)
            {
                fixedUpdateTime = 0.03f;
                IsTimeDelay = true;
                updateSpeed = 100;
                return;
            }
    }

}
