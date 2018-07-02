using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// question add
public class QuestionDiv : Questionary
{
    /// <summary>
    /// 
    /// </summary>
    public QuestionDiv(int quizcount)
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
                        GetQuestionNumber(3, 9,ref numdata);
                    }
                    break;
                case 1:
                    {                        
                        GetQuestionNumber(10, 30, ref numdata);
                    }
                    break;
                case 2:
                    {                        
                        GetQuestionNumber(10, 99, ref numdata);
                    }
                    break;
                case 3:
                    {                        
                        GetQuestionNumber(100, 999, ref numdata);
                    }
                    break;
            }
            CreateWrongAnswer(numdata);
            queuedata.Enqueue(numdata);
        }
    }

    private void GetQuestionNumber(int min, int max, ref MixNumber mixnumber)
    {
        while (true)
        {
            int rand = Random.Range(min,max);
            if( prime(rand) == false)
            {
                DivdCount(rand);
                if (gatheringAnswer.Count > 0)
                {
                    mixnumber.a = rand;                         
                    break;
                }
            }
        }
        int randindex = Random.Range(0, gatheringAnswer.Count - 1);
        mixnumber.b = gatheringAnswer[randindex];

        mixnumber.rightAnswerIndex = Random.Range(0, 3);
        mixnumber.Answer[mixnumber.rightAnswerIndex] = mixnumber.a / mixnumber.b;
    }

    // this number is prime
    private bool prime(int n)
    {
        for (int i = 3; i * i <= n; i++)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }

    private List<int> gatheringAnswer = new List<int>();
    private void DivdCount(int n)
    {
        gatheringAnswer.Clear();
        while (n > 1)
        {
            for (int i = 2; i <= n; i++)
            {
                if (n % i != 0)
                    continue;

                gatheringAnswer.Add(i);
                n = n / i;                    
                break;
            }
        }
    }

    public override void Clear()
    {
        base.Clear();
        if(gatheringAnswer != null)
        {
            gatheringAnswer.Clear();
            gatheringAnswer = null;
        }
    }

}
