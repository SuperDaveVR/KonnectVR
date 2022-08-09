using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessmentHandler : MonoBehaviour
{
    [SerializeField] private QuizObj QuestionsAnswered;

    public int QuestionsAnsweredCount
    {
        private set;
        get;
    }

    public void Setup(QuizObj quizObj)
    {
        if (quizObj.Name != QuestionsAnswered.Name)
        {
            Reset();
            QuestionsAnswered.Name = quizObj.Name;
        }
    }

    private void Reset()
    {
        QuestionsAnswered.QuestionsList.Clear();
    }

    public void AddQuestion(QuizQuestion question)
    {
        List<QuizQuestion> questionList = QuestionsAnswered.QuestionsList;
        bool exists = questionList.Contains(question);

        bool isAnswered = CheckIfAnswered(question);

        if (!exists)
        {
            if (isAnswered)
            {
                questionList.Add(question);
                QuestionsAnsweredCount++;
            }
        } else
        {
            if (!isAnswered)
            {
                questionList.Remove(question);
                QuestionsAnsweredCount--;
            }
        }
    }

    private bool CheckIfAnswered(QuizQuestion question)
    {
        List<QuizAnswer> answerList = question.Answers;
        bool isAnswered = false;

        foreach (QuizAnswer answer in answerList)
        {
            if (answer.IsSelected)
            {
                isAnswered = true;
                break;
            }
        }

        return isAnswered;
    }
}
