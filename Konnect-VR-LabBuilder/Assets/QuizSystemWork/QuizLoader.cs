using ExcelReadWrite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class QuizLoader : MonoBehaviour
{
    private ExcelReader excelReader = new ExcelReader();
    private DataTable settingTable;
    private DataTable quizTable;

    //Format Information
    [SerializeField] private string[] excelSheets = { "Settings", "Quiz" };

    public DataTable[] loadQuiz(string filePath)
    {
        if (excelReader.chooseFile(filePath))
        {
            string[] strSplit = { "Questions" };
            DataTable[] retTables = excelReader.ReadCSV(strSplit).ToArray();

            Debug.Log("Setting Table:" + retTables[0].Rows[0][retTables[0].Columns[0]].ToString());
            Debug.Log("Quiz Table Row 0, Col 0: " + retTables[1].Rows[0][retTables[1].Columns[0]].ToString());

            return retTables;
        }

        return null;
    }
}
