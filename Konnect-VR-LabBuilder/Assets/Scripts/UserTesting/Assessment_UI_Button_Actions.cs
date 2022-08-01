using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using KonnectVR.UserManagement;
using System.Text;
using System.IO;

namespace KonnectVR.UserTesting
{
    public class Assessment_UI_Button_Actions : MonoBehaviour
    {
        // Note that all occurences of GetComponent outside of the start method shall be assigned to private variables.

        //This script will handle the actions of each button in the Assessment UI
        public GameObject UI_Panel;
        public GameObject title_text;
        public GameObject next;
        public GameObject back;
        public GameObject submit;
        public GameObject answer_area;
        public GameObject progress_text;
        public GameObject questions_answered_text;
        public GameObject saved_text;
        private List<GameObject> question_panels_List;
        private int num;
        private int total;
        private TMP_Text prog_text;
        private TMP_Text save_text;
        private TMP_Text q_answered_text;
        private int[] questions_responded_to;
        private int index_num;
        private int number_of_questions_answered;
        private int score;
        private QuizAssessment quiz;
        private string userID;
        private string[][] FinalResults;
        private List<string[]> QuizResults = new List<string[]>();

        void Start()
        {
            quiz = GetComponent<QuizAssessment>();
            title_text.GetComponent<TMP_Text>().text = quiz.getQuizName();
            question_panels_List = quiz.getQuestions();
            num = 1;
            score = 0;
            total = question_panels_List.Count;
            prog_text = progress_text.GetComponent<TMP_Text>();
            save_text = saved_text.GetComponent<TMP_Text>();
            q_answered_text = questions_answered_text.GetComponent<TMP_Text>();
            questions_responded_to = new int[total];
            back.GetComponent<Button>().interactable = false;
            number_of_questions_answered = 0;

            prog_text.text = "Question " + num + " of " + total;
            q_answered_text.text = number_of_questions_answered + " of " + total + " Questions Answered";

            userID = GameObject.Find("CurrentUser").GetComponent<User>().getUserId();
        }

        public void next_button()
        {
            question_panels_List[num - 1].SetActive(false);
            num++;
            question_panels_List[num - 1].SetActive(true);
            is_question_answered();
            prog_text.text = "Question " + num + " of " + total;

            if (num == total)
            {
                next.GetComponent<Button>().interactable = false;
            }

            if (back.GetComponent<Button>().interactable == false)
            {
                back.GetComponent<Button>().interactable = true;
            }

        }

        public void back_button()
        {
            question_panels_List[num - 1].SetActive(false);
            num--;
            question_panels_List[num - 1].SetActive(true);
            is_question_answered();
            prog_text.text = "Question " + num + " of " + total;

            if (num == 1)
            {
                back.GetComponent<Button>().interactable = false;
            }

            if (next.GetComponent<Button>().interactable == false)
            {
                next.GetComponent<Button>().interactable = true;
            }

        }

        public void answer_question(bool value)
        {
            if (!questions_responded_to.Contains(num))
            {
                index_num = num - 1;
                questions_responded_to[index_num] = num;
                number_of_questions_answered++;
                save_text.text = "Response Saved"; // change text to say that the response has been saved
            }

            q_answered_text.text = number_of_questions_answered + " of " + total + " Questions Answered";


            //Add answer in the answer_field variable to the database once we've established a connection
        }

        public void submit_button()
        {
            /*score = 0;
            foreach (GameObject question in question_panels_List)
            {
                Question q = question.GetComponent<Question>();
                
                if (q.isAnswer1Correct() && q.isAnswer2Correct() && q.isAnswer3Correct() && q.isAnswer4Correct())
                {
                    score++;
                }
            }
            Debug.Log("Quiz score = " + score + " of out " + total);*/

            exportToFile();
            UI_Panel.SetActive(false);
        }

        public void is_question_answered()
        {
            if (questions_responded_to.Contains(num))
            {
                save_text.text = "Response Saved"; // change text to say that the response has been saved
            }
            else
            {
                save_text.text = ""; // change text to say that the response has been saved
            }
        }

        private string[][] exportToFile()
        {
            StringBuilder sb = new StringBuilder();

            string[][] output = new string[question_panels_List.Count + 4][];

            int length = output.GetLength(0);
            string delimiter = ",";

            int i = 1;
            double score = 0;
            string[] QuizResultsTemp = new string[8];
            QuizResultsTemp[0] = "Question Number";
            QuizResultsTemp[1] = "Question Type"; // Multi-select
            QuizResultsTemp[2] = "Question";
            QuizResultsTemp[3] = "Option 1"; // Blue (Option Selected)(Correct Answer)
            QuizResultsTemp[4] = "Option 2"; // Red (Option Not Selected)(Inorrect Answer)
            QuizResultsTemp[5] = "Option 3"; // Green (Option Not Selected)(Correct Answer)
            QuizResultsTemp[6] = "Option 4"; // Yellow (Option Selected)(Incorrect Answer)
            QuizResultsTemp[7] = "Credit Given"; //0.5
            QuizResults.Add(QuizResultsTemp);
            output[0] = QuizResults[0];
            sb.AppendLine(string.Join(delimiter, output[0]));

            foreach (GameObject question in question_panels_List)
            {
                Question q = question.GetComponent<Question>();
                QuizResultsTemp[0] = i.ToString();
                if (q.isMultiChoice())
                    QuizResultsTemp[1] = "Multi-Choice"; // Multi-select
                else
                    QuizResultsTemp[1] = "Multi-Select";
                QuizResultsTemp[2] = q.getQuestionText();
                QuizResultsTemp[3] = answersIntoResultsArray(q.getAnswer1(), q.answer1Value(), q.response1Selected()); // Blue (Option Selected)(Correct Answer)
                QuizResultsTemp[4] = answersIntoResultsArray(q.getAnswer2(), q.answer2Value(), q.response2Selected()); // Red (Option Not Selected)(Inorrect Answer)
                QuizResultsTemp[5] = answersIntoResultsArray(q.getAnswer3(), q.answer3Value(), q.response3Selected()); // Green (Option Not Selected)(Correct Answer)
                QuizResultsTemp[6] = answersIntoResultsArray(q.getAnswer4(), q.answer4Value(), q.response4Selected()); // Yellow (Option Selected)(Incorrect Answer)
                if (q.isMultiChoice())
                {
                    if (q.isAnswer1Correct() && q.isAnswer2Correct() && q.isAnswer3Correct() && q.isAnswer4Correct())
                    {
                        QuizResultsTemp[7] = "Full Credit";
                        score = score + 1;
                    }
                    else
                        QuizResultsTemp[7] = "No Credit";
                }
                else
                {
                    int numOfCorrectResponses = 0;
                    if (q.isAnswer1Correct())
                        numOfCorrectResponses++;
                    if (q.isAnswer2Correct())
                        numOfCorrectResponses++;
                    if (q.isAnswer3Correct())
                        numOfCorrectResponses++;
                    if (q.isAnswer4Correct())
                        numOfCorrectResponses++;
                    if (numOfCorrectResponses == 4)
                    {
                        QuizResultsTemp[7] = "Full Credit";
                        score = score + 1;
                    }
                    else if (numOfCorrectResponses == 3)
                    {
                        QuizResultsTemp[7] = ".75 Credit";
                        score = score + 0.75;
                    }
                    else if (numOfCorrectResponses == 2)
                    {
                        QuizResultsTemp[7] = ".5 Credit";
                        score = score + 0.5;
                    }
                    else if (numOfCorrectResponses == 1)
                    {
                        QuizResultsTemp[7] = ".25 Credit";
                        score = score + 0.25;
                    }
                    else
                        QuizResultsTemp[7] = "No Credit";
                }
                QuizResults.Add(QuizResultsTemp);
                output[i] = QuizResults[i];
                sb.AppendLine(string.Join(delimiter, output[i]));
                i++;
            }

            QuizResultsTemp[0] = "";
            QuizResultsTemp[1] = "";
            QuizResultsTemp[2] = "";
            QuizResultsTemp[3] = "";
            QuizResultsTemp[4] = "";
            QuizResultsTemp[5] = "";
            QuizResultsTemp[6] = "";
            QuizResultsTemp[7] = "";
            QuizResults.Add(QuizResultsTemp);
            output[output.Length - 3] = QuizResults[output.Length - 3];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 3]));

            QuizResultsTemp[0] = "Points Scored";
            QuizResultsTemp[1] = "Points Possible";
            QuizResults.Add(QuizResultsTemp);
            output[output.Length - 2] = QuizResults[output.Length - 2];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 2]));

            QuizResultsTemp[0] = "" + score;
            QuizResultsTemp[1] = "" + question_panels_List.Count;
            QuizResults.Add(QuizResultsTemp);
            output[output.Length - 1] = QuizResults[output.Length - 1];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 1]));

            Debug.Log("Quiz Score: " + score + " / " + question_panels_List.Count);

            string filePath = getPath();

            StreamWriter outStream = System.IO.File.CreateText(filePath);
            outStream.WriteLine(sb);
            outStream.Close();
            return output;

        }

        private string answersIntoResultsArray(string answer, bool isCorrect, bool wasSelected)
        {
            string response = answer;

            if (isCorrect && wasSelected)
                response = response + " (Option Selected) (Correct Answer)";
            else if (!isCorrect && wasSelected)
                response = response + " (Option Selected) (Incorrect Answer)";
            else if (isCorrect)
                response = response + " (Option Not Selected) (Correct Answer)";
            else
                response = response + " (Option Not Selected) (Incorrect Answer)";

            return response;
        }

        private string getPath()
        {
#if UNITY_EDITOR
            return Application.dataPath + "/Quiz Results/" + quiz.getQuizName() + "_" + userID + "_Quiz_Results.csv";
#elif UNITY_ANDROID
            return Application.persistentDataPath+ quiz.getQuizName() + "_" + userID + "_Quiz_Results.csv";
#elif UNITY_IPHONE
            return Application.persistentDataPath+ quiz.getQuizName() */ + "_" + userID + "_Quiz_Results.csv";
#else
            return Application.dataPath +"/Quiz Results/"+ quiz.getQuizName() + "_" + userID + "_Quiz_Results.csv";
#endif
        }

        /*private void get_questions_from_database() // since we don't have a database connection established, we're just hard-coding this in for now.
        {
            questions_from_database = new string[,]{ { "1", "This is the text of question 1", "Answer 1", "MC"}, 
                                                        { "2", "This is the text of question 2", "Answer 2", "Multi"},
                                                        { "3", "This is the text of question 3", "Answer 3", "MC"},
                                                        { "4", "This is the text of question 4", "Answer 4", "Multi"},
                                                        { "5", "This is the text of question 5", "Answer 5", "MC"},
                                                        { "6", "This is the text of question 6", "Answer 6", "Multi"},
                                                        { "7", "This is the text of question 7", "Answer 7", "MC"},
                                                        { "8", "This is the text of question 8", "Answer 8", "Multi"},
                                                        { "9", "This is the text of question 9", "Answer 9", "MC"},
                                                        { "10", "This is the text of question 10", "Answer 10", "Multi"},
                                                        { "11", "This is the text of question 11", "Answer 11", "MC"},
                                                        { "12", "This is the text of question 12", "Answer 12", "Multi"},
                                                        { "13", "This is the text of question 13", "Answer 13", "MC"},
                                                        { "14", "This is the text of question 14", "Answer 14", "Multi"},
                                                        { "15", "This is the text of question 15", "Answer 15", "MC"},
                                                        { "16", "This is the text of question 16", "Answer 16", "Multi"},
                                                        { "17", "This is the text of question 17", "Answer 17", "MC"},
                                                        { "18", "This is the text of question 18", "Answer 18", "Multi"},
                                                        { "19", "This is the text of question 19", "Answer 19", "MC"},
                                                        { "20", "This is the text of question 20", "Answer 20", "Multi"}};

        }*/

        /*private void MC_or_Multi(int i, GameObject multi, GameObject MC)
        {
            if (questions_from_database[i, 3] == "MC")
            {
                multi.SetActive(false);
                MC.SetActive(true);
            }
            else
            {
                MC.SetActive(false);
                multi.SetActive(true);
            }
        }*/


    }
}
