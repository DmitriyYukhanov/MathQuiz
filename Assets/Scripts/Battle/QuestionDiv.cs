using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// question add
public class QuestionDiv : Questionary
{
    /// <summary>
    /// 
    /// </summary>
    public QuestionDiv()
    {
        queuedata = new Queue<MixNumber>();

        for (int i = 0; i < 10; i++)
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

            numdata.c = numdata.a / numdata.b;
            queuedata.Enqueue(numdata);
        }
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


    private void DivdCount(int n)
    {

        while (n > 1)
        {
            for (int i = 2; i <= n; i++)
            {
                if (n % i == 0)
                {
                    n = n / i;
                    // i =  to combine count
                    break;
                }
            }

        }
    }

}
