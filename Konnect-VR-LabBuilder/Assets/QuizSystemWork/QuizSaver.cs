using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelReadWrite;
using System.Data;
using System;

public class QuizSaver : MonoBehaviour
{
    ExcelWriter excelWriter = new ExcelWriter();

    //Format Information
    [SerializeField] private string[] excelSheets = {"Settings", "Quiz"};
    [SerializeField] private string[] sheetFields = {"`Quiz Name` varchar(255), `Randomize Question Order` varchar(5), `Randomize Answer Order` varchar(5)", //Settings data fields
                                    "`Question Type` varchar(50), `Question` varchar(255), `Choose Multiple?` varchar(5), `Highlight?` varchar(5), `Highlighted Object` varchar(255), `Options` varchar(255), `Correct Answer` varchar(255), `Point Value` int(2)"}; //Quiz Data fields

    public void saveQuiz(QuizObj quiz, string filePath)
    {
        DataTable settingData = createSettingTable(quiz);
        DataTable quizData = createQuizTable(quiz);

        if (excelWriter.chooseFile(filePath))
        {
            DataTable[] sendTables = { settingData, quizData };
            string[] headers = { "Questions" };
            excelWriter.writeCSV(sendTables, headers);
        }
    }

    private DataTable createSettingTable(QuizObj quiz)
    {
        DataTable settingsTable = new DataTable("Settings");
        DataColumn dtColumn;
        DataRow dtRow;

        //Create Quiz Name Column
        dtColumn = createColumn("System.String", "Quiz Name");
        settingsTable.Columns.Add(dtColumn);

        //Create Randomize Question Order Column
        dtColumn = createColumn("System.Boolean", "Randomize Question Order");
        settingsTable.Columns.Add(dtColumn);

        //Create Randomize Answer Order Column
        dtColumn = createColumn("System.Boolean", "Randomize Answer Order");
        settingsTable.Columns.Add(dtColumn);

        //Create Row
        dtRow = settingsTable.NewRow();
        dtRow["Quiz Name"] = quiz.Name;
        dtRow["Randomize Question Order"] = quiz.RandomizeQuestions;
        dtRow["Randomize Answer Order"] = quiz.RandomizeAnswers;

        settingsTable.Rows.Add(dtRow);

        return settingsTable;
    }

    private DataTable createQuizTable(QuizObj quiz)
    {
        DataTable quizTable = new DataTable("Quiz");
        DataColumn dtColumn;
        DataRow dtRow;

        //Create Quiz Type Column
        dtColumn = createColumn("System.String", "Question Type");
        quizTable.Columns.Add(dtColumn);

        //Create Question Column
        dtColumn = createColumn("System.String", "Question");
        quizTable.Columns.Add(dtColumn);

        //Create Choose Multiple Column
        dtColumn = createColumn("System.Boolean", "Choose Multiple");
        quizTable.Columns.Add(dtColumn);

        //Create Highlight? Column
        dtColumn = createColumn("System.Boolean", "Highlight?");
        quizTable.Columns.Add(dtColumn);

        //Create Highlighted Object Column
        dtColumn = createColumn("System.String", "Highlighted Object");
        quizTable.Columns.Add(dtColumn);

        //Create Options Column
        dtColumn = createColumn("System.String", "Options");
        quizTable.Columns.Add(dtColumn);

        //Create Correct Answer Column
        dtColumn = createColumn("System.String", "Correct Answer");
        quizTable.Columns.Add(dtColumn);

        //Create Point Value Column
        dtColumn = createColumn("System.Int16", "Point Value");
        quizTable.Columns.Add(dtColumn);

        //Create Rows
        List<QuizQuestion> questions = quiz.QuestionsList;

        foreach (QuizQuestion question in questions)
        {
            dtRow = quizTable.NewRow();
            dtRow["Question Type"] = question.Type;
            dtRow["Question"] = question.Text;
            dtRow["Choose Multiple"] = question.ChooseMultiple;
            dtRow["Highlight?"] = question.Highlight;
            dtRow["Highlighted Object"] = question.HighlightedObjName;
            dtRow["Options"] = question.OptionsText;
            dtRow["Correct Answer"] = question.CorrectText;
            dtRow["Point Value"] = question.PointValue;

            quizTable.Rows.Add(dtRow);
        }

        return quizTable;
    }

    private DataColumn createColumn(string dataType, string columnName)
    {
        DataColumn dtColumn;
        dtColumn = new DataColumn();
        dtColumn.DataType = Type.GetType(dataType);
        dtColumn.ColumnName = columnName;
        dtColumn.Caption = columnName;

        return dtColumn;
    }
}
