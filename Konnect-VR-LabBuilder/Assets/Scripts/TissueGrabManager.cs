using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TissueGrabManager : MonoBehaviour
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
    public TextMeshProUGUI feedbackText;
    public GameObject newQuestions;

    public AudioSource correct;
    public AudioSource incorrect;

    //This will allow the script to call a method from another script 
    public QuestionManager questionManager;

    
    private int chosenQuestion;

    private List<int> possibleQuestion;
    // Start is called before the first frame update
    void Start()
    {

        questionManager = GameObject.FindObjectOfType(typeof(QuestionManager)) as QuestionManager;

        feedbackText.text = "";

        possibleQuestion = new List<int>();

        newQuestions.SetActive(false);
        numberOfQuestions = 6;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            possibleQuestion.Add(i);
        }

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







        System.Random r = new System.Random();
        int index = r.Next(0, possibleQuestion.Count);
        chosenQuestion = possibleQuestion[index];
        possibleQuestion.RemoveAt(index);

        questionIndex = 0;
        score = 0;
        questionText.text = questions[chosenQuestion];
       // tissueSets[questionIndex].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStart()
    {
        questionManager = GameObject.FindObjectOfType(typeof(QuestionManager)) as QuestionManager;

        feedbackText.text = "";

        possibleQuestion = new List<int>();

        newQuestions.SetActive(false);
        numberOfQuestions = 6;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            possibleQuestion.Add(i);
        }

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









        System.Random r = new System.Random();
        int index = r.Next(0, possibleQuestion.Count);
        chosenQuestion = possibleQuestion[index];
        possibleQuestion.RemoveAt(index);

        questionIndex = 0;
        score = 0;
        questionText.text = questions[chosenQuestion];
        // tissueSets[questionIndex].SetActive(true);
    }

    public void setSelectedTissue(GameObject tissue)
    {
        selectedTissue = tissue;
    }

    public void processAnswer()
    {
        //Check if the current grabbed tissue is the correct answer
        if(questionIndex == -1)
        {

        }
        else
        {

        
            if (selectedTissue.tag == answers[chosenQuestion])
            {
                score++;
                scoreText.text = "Score: " + score;
                feedbackText.text = "Correct!";
                correct.Play();
            }
            else
            {
                feedbackText.text = "Incorrect, you grabbed the " + selectedTissue.tag;
                incorrect.Play();
            }


        
            questionIndex++;
            if (questionIndex <= (questions.Length - 1))
            {
                //questionIndex++;
                //Activate tissue  for the next question
                System.Random r = new System.Random();
            int index = r.Next(0, possibleQuestion.Count);
            chosenQuestion = possibleQuestion[index];
            possibleQuestion.RemoveAt(index);
                questionText.text = questions[chosenQuestion];
            }

            if (questionIndex >= questions.Length)
            {
                questionText.text = "";
                quizCompleteText.text = "Quiz completed. Your score is: " + score;
                questionIndex = -1;
                newQuestions.SetActive(true);
            }

        }

    }

    public void SetNewQuestions()
    {
        feedbackText.text = "";
        newQuestions.SetActive(false);
        scoreText.text = "Score: " + score;
        questionManager.showAll();

    }

}
