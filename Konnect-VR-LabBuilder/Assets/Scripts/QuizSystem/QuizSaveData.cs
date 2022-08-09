using System;
using System.Collections.Generic;

[Serializable]
public class QuizSaveData
{
    public string quizName;
    public List<QuizQuestion> questions;
    public bool randomizeQuestionOrder;
    public bool randomizeAnswerOrder;
}