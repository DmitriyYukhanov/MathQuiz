using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public enum QuestionType
{
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

public class Questionary
{
    protected Queue<MixNumber> queuedata;

    protected MixNumber currentQuiz;

    public virtual void Clear()
    {
        queuedata.Clear();
        queuedata = null;
    }

    // correct answer
    public bool IsCorrectAnswer(int answerindex)
    {
        if (currentQuiz.rightAnswerIndex == answerindex)
            return true;

        return false;
    }

    // next quiz
    public MixNumber NextQuiz()
    {
        // nomore quiz
        if (queuedata.Count <= 0)
            return null;

        currentQuiz = queuedata.Dequeue();
        return currentQuiz;
    }

    protected void CreateWrongAnswer(MixNumber mixnumber)
    {
        int randAnswer = -1;
        for (int i = 0; i < mixnumber.Answer.Length; i++)
        {
            if (mixnumber.Answer[i] != -1)
                randAnswer = mixnumber.Answer[i];
        }

        for (int i = 0; i < mixnumber.Answer.Length; i++)
        {
            if (mixnumber.Answer[i] == -1)
            {
                int index = Random.Range(0, 2);
                switch (index)
                {
                    case 0:
                        mixnumber.Answer[i] = randAnswer + Random.Range(1, 100);
                        break;
                    case 1:
                        mixnumber.Answer[i] = randAnswer * Random.Range(2, 30);
                        break;
                    case 2:
                        mixnumber.Answer[i] = randAnswer - Random.Range(1, 100);
                        break;
                }
            }

        }
    }
}