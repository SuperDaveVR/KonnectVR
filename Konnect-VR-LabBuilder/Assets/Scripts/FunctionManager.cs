using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunctionManager : MonoBehaviour
{

    private string[] questions;
    private string[] answers;
    int numberOfQuestions;
    private int questionIndex;
    private GameObject selectedTissue;
    private int score;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI quizCompleteText;
    public GameObject newQuestions;
    public QuestionManager questionManager;



    // Start is called before the first frame update
    void Start()
    {

        questionManager = GameObject.FindObjectOfType(typeof(QuestionManager)) as QuestionManager;

        newQuestions.SetActive(false);
        numberOfQuestions = 6;

        //Initialize all questions
        questions = new string[numberOfQuestions];

        answers = new string[numberOfQuestions];
        //initialize the answer to the above questions. The index of the answers array cooresponds with the index of the questions array

        //Question and answer for question 1
        questions[0] = "Grab the Periosteum";
        answers[0] = "Periosteum";

        //Question and answer for question 2
        questions[1] = "Grab the  Haversian Canal. ";
        answers[1] = "HaversianCanal";

        //Question and answer for question 3
        questions[2] = "Grab the Volkmann Canal. ";
        answers[2] = "VolkmannCanal";

        //Question and answer for question 4
        questions[3] = "Which of these is the Trabecula? Grab the correct one.";
        answers[3] = "Trabecula";

        //Question and answer for question 5
        questions[4] = "Which of these structues are Osteocytes located? Grab the correct one.";
        answers[4] = "Lacuna";

        //Question and answer for question 6
        questions[5] = "Grab the unit structure of compact bones";
        answers[5] = "CompactBoneStructure";









        questionIndex = 0;
        score = 0;
        questionText.text = questions[questionIndex];
        // tissueSets[questionIndex].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStart()
    {
        questionIndex = 0;
        score = 0;
        questionText.text = questions[questionIndex];
    }

    public void setSelectedTissue(GameObject tissue)
    {
        selectedTissue = tissue;
    }

    public void processAnswer()
    {
        //Check if the current grabbed tissue is the correct answer

        if (selectedTissue.tag == answers[questionIndex])
        {
            score++;
            scoreText.text = "Score: " + score;
        }



        questionIndex++;
        if (questionIndex <= (questions.Length - 1))
        {
            //questionIndex++;
            //Activate tissue  for the next question
            questionText.text = questions[questionIndex];
        }

        if (questionIndex >= questions.Length)
        {
            questionText.text = "";
            quizCompleteText.text = "Quiz completed. Your score is: " + score;
            scoreText.text = "";
            newQuestions.SetActive(true);
        }
    }

    public void SetNewQuestions()
    {
        score = 0;
        questionIndex = 0;
        newQuestions.SetActive(false);
        scoreText.text = "Score: " + score;
        questionManager.showAll();

    }
}
