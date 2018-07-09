using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

// Closure 
public struct IndexClosure
{
    public int Index;
}

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

    // readyCountv
    [SerializeField]
    private UnityEngine.UI.Text readyCountLabel;
    //==========================================================
    // variable
    //==========================================================

    // quiz data and information
    private MakeQuestion quiz;

    // button delay
    private bool IsAction = false;

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

    public void Init(QuestionType quetionType)
    {
        quiz = new MakeQuestion(quetionType, 3);
        for (int i = 0; i < tweenAnswerLabel.Length; i++)
        {
            startTime = tweenAnswerLabel[i].GetAnimationDuration();
            UnityEngine.UI.Button buton = tweenAnswerLabel[i].gameObject.GetComponent<UnityEngine.UI.Button>();

            IndexClosure indexclosure;
            indexclosure.Index = i;
            buton.onClick.RemoveAllListeners();
            buton.onClick.AddListener(delegate
            {   
                OnPressedEvent(indexclosure.Index);
            });
        }

        OnExitEventHandler -= OnExitEvent;
        OnExitEventHandler += OnExitEvent;

        SetPoint(0);
        SetReadyCount(3, true);
        SetTime(quiz.TimeSec);
    }

    public void Clear()
    {
        quiz.Clear();
    }

    public void StartGame()
    {
        StartCoroutine(IEStartGame());
    }

    private IEnumerator IEStartGame()
    {
        int readyCount = 3;
        while (readyCount > 0)
        {
            yield return new WaitForSeconds(0.5f);
            readyCount--;
            SetReadyCount(readyCount, true);
        }

        SetReadyCount(0, false);
        Invoke("InvokeNextQuiz", startTime + 0.03f);
    }

    private bool InvokeNextQuiz()
    {
        if (quiz.SetNextQuestion(questionLabel, answerLabels) == false)
        {
            // =================
            // game over dealing
            // =================
            // Close
            InitAnimation();
            return false;
        }

        StartAnimation();
        return true;
    }

    // init animaation set
    private void InitAnimation()
    {
        tweenQuizLabel[0].SetStartValues();        
        for (int i = 0; i < tweenAnswerLabel.Length; i++)
        {
            tweenAnswerLabel[i].SetStartValues();            
        }
    }

    // start animation
    private void StartAnimation()
    {
        tweenQuizLabel[0].SetStartValues();
        tweenQuizLabel[0].ChangeSetState(false);
        tweenQuizLabel[0].OpenCloseObjectAnimation();
        float startActiveTime = tweenQuizLabel[0].GetAnimationDuration();

        for (int i = 0; i < tweenAnswerLabel.Length; i++)
        {
            tweenAnswerLabel[i].SetStartValues();
            tweenAnswerLabel[i].ChangeSetState(false);
            tweenAnswerLabel[i].OpenCloseObjectAnimation();
            tweenAnswerLabel[i].GetAnimationDuration();
        }

        Invoke("InvokeStartActive", startActiveTime + 0.1f);
    }

    // button Event
    private void OnPressedEvent(int index)
    {
        if (IsAction == false)
            return;
        
        bool IsCorrect = quiz.CorrectAnswer(index);
        NextQuizAnimated();

        if (IsCorrect)
        {
            quiz.CalcuratePoint();
        }        
    }

    // next quize animated
    private void NextQuizAnimated()
    {
        IsAction = false;
        quiz.ResetTime();
        SetTime(quiz.TimeSec);

        tweenQuizLabel[1].SetStartValues();
        tweenQuizLabel[1].ChangeSetState(false);
        tweenQuizLabel[1].OpenCloseObjectAnimation();
        float NextTime = tweenQuizLabel[1].GetAnimationDuration();
        Invoke("InvokeNextQuiz", NextTime + 0.1f);

    }


    // button Event Active
    private void InvokeStartActive()
    {
        IsAction = true;
    }

    // onApp Pause don't cheat.
    void OnApplicationPause(bool pauseStatus)
    {
        IsForceFail = true;
    }

    private void SetReadyCount(int sec, bool IsActive)
    {
        readyCountLabel.gameObject.SetActive(IsActive);
        readyCountLabel.text = sec.ToString();
        if (sec == 3)
            readyCountLabel.color = GetColor(91, 238, 220, 255); 
        else if(sec == 2)
            readyCountLabel.color = GetColor(238, 198, 91, 255);
        else
            readyCountLabel.color = GetColor(241, 161, 249, 255);
    }

    private Color GetColor (int r, int g, int b, int a)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    private void SetPoint(int point)
    {
        pointLabel.text = string.Format("{0:#,###0}", point);
    }

    private void SetTime(float time)
    {
        secLabel.text = string.Format("{0:f3}", time);
    }


    private void Update()
    {
        float deltatime = Time.deltaTime;

        UpdateLimitTime(deltatime);
        ResetUpdatePointTime(deltatime);
    }

    // udpate limit time
    private void UpdateLimitTime(float deltatime)
    {
        if (IsAction == false)
            return;

        limitTime += deltatime;
        if (limitTime >= 1.0f)
            limitTime = 0.000f;

        if (quiz.UpdateDownTime(deltatime))
            NextQuizAnimated();

        SetTime(quiz.TimeSec);
    }

    //
    private void ResetUpdatePointTime(float deltatime)
    {
        if (IsUpdatePoint)
        {
            updatePointTime += deltatime;
            if (updatePointTime >= fixedUpdateTime)
            {
                updatePointTime = 0f;
                UpdateSpeedPointTime(targetUpdateMaxPoint, updatePoint);
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


    /// <summary>
    /// 시간이 거의 다되었을 경우 스피드를 올려주어 해당값으로 빨리 가도록 한다.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    private void UpdateSpeedPointTime(int a, int b)
    {
        if (IsTimeDelay)
            return;

        if (a > b && a - b <= 2000)
        {
            fixedUpdateTime = 0.03f;
            IsTimeDelay = true;
            updateSpeed = 100;
            return;
        }

    }

    //ExitEvent
    private void OnExitEvent()
    {
        if (parentOpenMeUI != null)
            parentOpenMeUI.Open();
    }
}
