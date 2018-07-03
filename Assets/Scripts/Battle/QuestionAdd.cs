using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        for (int  i = 0; i< mixnumber.Answer.Length; i++)
        {
            if(mixnumber.Answer[i] == -1)
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

// question add
public class QuestionAdd : Questionary
{
    /// <summary>
    /// 
    /// </summary>
    public QuestionAdd(int quizcount)
    {
        queuedata = new Queue<MixNumber>();

        for (int i = 0; i < quizcount; i++)
        {
            MixNumber numdata = new MixNumber();

            int digit = Random.Range(0, 3);
            switch (digit)
            {
                case 0:
                    {
                        numdata.a = Random.Range(0, 9);
                        numdata.b = Random.Range(0, 9);                        
                    }
                    break;
                case 1:
                    {
                        numdata.a = Random.Range(0, 10);
                        numdata.b = Random.Range(10, 99);
                    }
                    break;
                case 2:
                    {
                        numdata.a = Random.Range(10, 99);
                        numdata.b = Random.Range(10, 99);
                    }
                    break;
                case 3:
                    {
                        numdata.a = Random.Range(100, 999);
                        numdata.b = Random.Range(100, 999);
                    }
                    break;
            }

            numdata.rightAnswerIndex = Random.Range(0, 3);
            numdata.Answer[numdata.rightAnswerIndex] = numdata.a + numdata.b;
            CreateWrongAnswer(numdata);
            
            queuedata.Enqueue(numdata);
        }
    }

    

}
