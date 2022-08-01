using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizFileManager : AFileManager
{
    [SerializeField] private QuizSaver quizSaver;
    [SerializeField] private QuizLoader quizLoader;
    [SerializeField] private GameObject quizSystem;
    [SerializeField] private GameObject quizPrefab;
    [SerializeField] private QuizObj quiz;
    [SerializeField] private FileSelectionScreen fileSelectionScreen;
    [SerializeField] private string compatibleCSVExtensions = "*.csv";
    [SerializeField] private string compatibleDataExtensions = "*.dat";
    [SerializeField] private string defaultSaveExtension = ".csv";

    [SerializeField] private string LabFileManagerName = "LabFileManager";

    public enum LoadMode { CSV, File, Blank };

    [SerializeField] private LoadMode loadMode = LoadMode.CSV;

    private void Start()
    {
        if (quizSystem == null)
        {
            quizSystem = this.gameObject;
        }

        if (quizSaver == null)
        {
            quizSaver = quizSystem.GetComponent<QuizSaver>();
        }

        if (quizLoader == null)
        {
            quizLoader = quizSystem.GetComponent<QuizLoader>();
        }

        if (saveSerial == null)
        {
            saveSerial = GameObject.Find(LabFileManagerName).GetComponent<SaveSerial>();
        }
    }

    public override void Load(string filePath)
    {
        base.Load(filePath);

        switch(loadMode)
        {
            case LoadMode.CSV:
                FromCSV(filePath);
                break;

            case LoadMode.File:
                FromFile(filePath);
                break;

            case LoadMode.Blank:
                BlankQuiz();
                break;
        }
    }

    public override void Save(string filePath)
    {
        base.Save(filePath);
        quizSaver.saveQuiz(quiz, filePath);
    }

    public override string GetDefaultExtension()
    {
        string ext = ".csv";
        return ext;
    }

    public void SelectQuiz(QuizObj quiz)
    {
        this.quiz = quiz;
    }

    public void ActivateFileSelectionScreen(string mode, string previousScreen) {
        string name = "";
        if (quiz != null && quiz.Name != null)
        {
            name = quiz.Name;
        }
        if (loadMode == LoadMode.File)
            fileSelectionScreen.Activate(this, mode, compatibleDataExtensions, defaultSaveExtension, name, previousScreen);
        else
            fileSelectionScreen.Activate(this, mode, compatibleCSVExtensions, defaultSaveExtension, name, previousScreen);
    }

    public void SetMode(string loadMode)
    {
        if (Enum.IsDefined(typeof(LoadMode), loadMode)) {
            SetMode((LoadMode)Enum.Parse(typeof(LoadMode), loadMode));
        }
    }

    public void SetMode(LoadMode loadMode)
    {
        this.loadMode = loadMode;
    }

    private void FromCSV(string filePath)
    {
        BlankQuiz();
        loadMode = LoadMode.CSV; 
        quiz.LoadFromExcel(filePath);
    }

    private void FromFile(string filePath)
    {
        //TODO: Set up .dat file
        loadMode = LoadMode.File;
        saveSerial.LoadQuizzesOnly(filePath);
    }

    private void BlankQuiz()
    {
        GameObject newQuiz = Instantiate(quizPrefab, quizSystem.transform);
        quiz = newQuiz.GetComponent<QuizObj>();
    }
}
