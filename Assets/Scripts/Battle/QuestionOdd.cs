using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// question add
public class QuestionOdd : Questionary
{
    /// <summary>
    /// 
    /// </summary>
    public QuestionOdd(int quizcount)
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
            numdata.Answer[numdata.rightAnswerIndex] = numdata.a - numdata.b;
            CreateWrongAnswer(numdata);
            queuedata.Enqueue(numdata);
        }
    }



}
