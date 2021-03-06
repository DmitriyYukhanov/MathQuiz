﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class MakeQuestion
{
    // game type
    private QuestionType quetionType;

    // question
    private Questionary questionary;

    // point stack
    private Queue<int> queuePoint = new Queue<int>();

    private readonly ObscuredFloat limitTime = 2.000f;

    // limit time
    private ObscuredFloat _timeSec;
    public ObscuredFloat TimeSec { get { return _timeSec; }}

    public void ResetTime()
    {
        _timeSec = limitTime;
    }

    public bool UpdateDownTime(float deltaTime)
    {
        _timeSec -= deltaTime;
        if (_timeSec <= 0.0f)                    
            return true;
        return false;        
    }

    // check directing point UI
    public bool IsEnablePoint{
        get { return queuePoint.Count > 0 ? true : false; }
    }

    public MakeQuestion(QuestionType type, int quizcount)
    {
        _timeSec = limitTime;
        quetionType = type;
        switch (type)
        {
            case QuestionType.ADD:
                questionary = new QuestionAdd(quizcount);
                break;
            case QuestionType.ODD:
                questionary = new QuestionOdd(quizcount);
                break;
            case QuestionType.DIV:
                questionary = new QuestionDiv(quizcount);
                break;
            case QuestionType.MUL:
                questionary = new QuestionMul(quizcount);
                break;            
        }
    }

    public void Clear()
    {
        if(questionary != null)
            questionary.Clear();

        questionary = null;
    }

    // next quiz setting
    public bool SetNextQuestion(UnityEngine.UI.Text quizlabel, UnityEngine.UI.Text[] answerlabels)
    {
        MixNumber mixdata = questionary.NextQuiz();

        if (mixdata == null)
            return false;
        
        _timeSec = limitTime;

        switch (quetionType)
        {
            case QuestionType.ADD:
                quizlabel.text = string.Format(" {0} + {1} = ? ", mixdata.a, mixdata.b);
                break;
            case QuestionType.ODD:
                quizlabel.text = string.Format(" {0} - {1} = ? ", mixdata.a, mixdata.b);
                break;
            case QuestionType.DIV:
                quizlabel.text = string.Format(" {0} / {1} = ? ", mixdata.a, mixdata.b);
                break;
            case QuestionType.MUL:
                quizlabel.text = string.Format(" {0} X {1} = ? ", mixdata.a, mixdata.b);
                break;
        }

        for (int i = 0; i < answerlabels.Length; i++)
        {
            answerlabels[i].text = mixdata.Answer[i].ToString();
        }

        return true;
    }

    // correct answer
    public bool CorrectAnswer(int answeIndex)
    {
        return questionary.IsCorrectAnswer(answeIndex);
    }

    // get point
    public int DequePoint(){
        if (queuePoint.Count <= 0)
            return 0;
        return queuePoint.Dequeue();
    }

    // calcurate point
    public void CalcuratePoint(){
        queuePoint.Enqueue(10000);
    }

  
}
