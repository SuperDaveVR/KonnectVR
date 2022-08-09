using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuizObj: MonoBehaviour
{

    [SerializeField] private string quizName;
    [SerializeField] private List<QuizQuestion> questions;
    [SerializeField] private int score;
    [SerializeField] private bool randomizeQuestionOrder;
    [SerializeField] private bool randomizeAnswerOrder;

    public string Name { 
        get { return quizName; } 
        set { quizName = value; }
    }

    public List<QuizQuestion> QuestionsList { get { return questions; } }
    public int Score { get { return score; } }
    public bool RandomizeQuestions { get { return randomizeQuestionOrder; } }
    public bool RandomizeAnswers { get { return randomizeAnswerOrder; } }
    public int Length { get { return questions.Count; } }

    public void LoadFromSaveData(QuizSaveData quizObj)
    {
        quizName = quizObj.quizName;
        questions = quizObj.questions;
        randomizeQuestionOrder = quizObj.randomizeQuestionOrder;
        randomizeAnswerOrder = quizObj.randomizeAnswerOrder;

        Setup();
    }

    public void Setup()
    {
        if (randomizeQuestionOrder)
        {
            shuffleQuestions();
        }
        if (randomizeAnswerOrder)
        {
            foreach (QuizQuestion question in questions)
            {
                System.Random random = new System.Random();
                question.shuffleAnswers(random);
            }
        }
    }

    public QuizQuestion GetQuestion(int questionNum)
    {
        QuizQuestion question = questions[questionNum];

        return question;
    }

    public void LoadFromExcel(string filePath)
    {
        QuizLoader loader = this.gameObject.GetComponentInParent<QuizLoader>();

        if (loader)
        {
            //Settings table is in index 0
            //Quiz table is in index 1
            DataTable[] quizTables = loader.loadQuiz(filePath);

            SetExcelSettings(quizTables[0]);
            SetExcelQuestions(quizTables[1]);

            Setup();
        } else
        {
            //Throw Error
            Debug.Log("No loader detected!");
        }
    }

    private void SetExcelSettings(DataTable settingTable)
    {
        quizName = settingTable.Rows[0][settingTable.Columns[0]].ToString();
        randomizeQuestionOrder = StrToBool(settingTable.Rows[0][settingTable.Columns[1]].ToString());
        randomizeAnswerOrder = StrToBool(settingTable.Rows[0][settingTable.Columns[2]].ToString());
    }

    private void SetExcelQuestions(DataTable questionTable)
    {
        QuizQuestion question;

        for (int i = 0; i < questionTable.Rows.Count; i++)
        {
            question = new QuizQuestion();
            question.BuildFromExcel(questionTable.Rows[i]);
            questions.Add(question);
        }
    }

    private bool StrToBool(string boolString)
    {
        bool retBool;

        string trimmed = String.Concat(boolString.Where(c => !Char.IsWhiteSpace(c)));

        retBool = trimmed.ToLower().Equals("true");

        return retBool;
    }

    private void shuffleQuestions()
    {
        System.Random random = new System.Random();
        questions = questions.OrderBy(item => random.Next()).ToList();
    }

    public QuizSaveData GetSaveData()
    {
        QuizSaveData quizSaveData = new QuizSaveData();
        quizSaveData.quizName = Name;
        quizSaveData.questions = QuestionsList;
        quizSaveData.randomizeQuestionOrder = RandomizeQuestions;
        quizSaveData.randomizeAnswerOrder = RandomizeAnswers;

        return quizSaveData;
    }
}
