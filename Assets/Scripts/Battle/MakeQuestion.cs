using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionType{
    ADD,
    ODD,
    DIV,
    MUL,
}

public class MixNumber
{
    public int a;
    public int b;
    public int c;

}

public class MakeQuestion
{
    // game type
    private QuestionType quetionType;

    private Questionary questionary;

    public MakeQuestion(QuestionType type)
    {        
        switch (type)
        {
            case QuestionType.ADD:
                questionary = new QuestionAdd();
                break;
            case QuestionType.ODD:
                questionary = new QuestionOdd();
                break;
            case QuestionType.DIV:
                questionary = new QuestionDiv();
                break;
            case QuestionType.MUL:
                questionary = new QuestionMul();
                break;            
        }
    }

    public void Clear()
    {
        if(questionary != null)
            questionary.Clear();
    }
    
    // correct answer
    public bool CorrectAnswer(int answer)
    {
        return questionary.IsCorrectAnswer(answer);
    }
    
  
}
