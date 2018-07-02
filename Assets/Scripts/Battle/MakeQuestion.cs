using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public enum QuestionType{
    ADD,
    ODD,
    DIV,
    MUL,
}

public class MixNumber
{
    public ObscuredInt a; 
    public ObscuredInt b;
    public ObscuredInt rightAnswerIndex;

    public ObscuredInt[] Answer = new ObscuredInt[4];    

    public MixNumber()
    {
        for (int i = 0; i < Answer.Length; i++)
            Answer[i] = -1;
    }
}

public class MakeQuestion
{
    // game type
    private QuestionType quetionType;

    // question
    private Questionary questionary;

    public MakeQuestion(QuestionType type, int quizcount)
    {
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
    
  
}
