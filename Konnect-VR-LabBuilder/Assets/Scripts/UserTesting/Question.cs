using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KonnectVR.UserTesting
{
    public class Question : MonoBehaviour
    {
        private QuestionScriptableObject questionSO;

        public TMP_Text qText;

        public GameObject MCPanel;
        public GameObject MultiSelectPanel;

        public GameObject MCAnswer1;
        public GameObject MCAnswer2;
        public GameObject MCAnswer3;
        public GameObject MCAnswer4;

        public GameObject MSAnswer1;
        public GameObject MSAnswer2;
        public GameObject MSAnswer3;
        public GameObject MSAnswer4;

        private questionType QuestionType;

        public TMP_Text QuestionText;

        private bool IsResponse1Correct;
        private TMP_Text ResponseOption1;

        private bool IsResponse2Correct;
        private TMP_Text ResponseOption2;

        private bool IsResponse3Correct;
        private TMP_Text ResponseOption3;

        private bool IsResponse4Correct;
        private TMP_Text ResponseOption4;


        public void setQuestionUI()
        {
            QuestionType = questionSO.type;

            QuestionText.text = questionSO.QuestionText;

            qText.text = getQuestionText();


            if (isMultiChoice())
            {
                MCPanel.SetActive(true);
                MultiSelectPanel.SetActive(false);
                ResponseOption1 = MCAnswer1.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption2 = MCAnswer2.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption3 = MCAnswer3.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption4 = MCAnswer4.transform.Find("Label").GetComponent<TMP_Text>();
                
            }

            else
            {
                MultiSelectPanel.SetActive(true);
                MCPanel.SetActive(false);
                ResponseOption1 = MSAnswer1.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption2 = MSAnswer2.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption3 = MSAnswer3.transform.Find("Label").GetComponent<TMP_Text>();
                ResponseOption4 = MSAnswer4.transform.Find("Label").GetComponent<TMP_Text>();
            }


            ResponseOption1.text = questionSO.answer1.getAnswer();
            IsResponse1Correct = questionSO.answer1.getIsCorrect();

            ResponseOption2.text = questionSO.answer2.getAnswer();
            IsResponse2Correct = questionSO.answer2.getIsCorrect();

            ResponseOption3.text = questionSO.answer3.getAnswer();
            IsResponse3Correct = questionSO.answer3.getIsCorrect();


            ResponseOption4.text = questionSO.answer4.getAnswer();
            IsResponse4Correct = questionSO.answer4.getIsCorrect();
        }
        
        public string getQuestionText()
        {
            return QuestionText.text;
        }

        public string getAnswer1()
        {
            return ResponseOption1.text;
        }

        public bool answer1Value()
        {
            return IsResponse1Correct;
        }
        public bool isAnswer1Correct()
        {
            return (response1Selected() == IsResponse1Correct);
        }
        public bool response1Selected()
        {
            if (isMultiChoice()) 
                return MCAnswer1.GetComponent<Toggle>().isOn;
            else
                return MSAnswer1.GetComponent<Toggle>().isOn;
        }
        public string getAnswer2()
        {
            return ResponseOption2.text;
        }

        public bool answer2Value()
        {
            return IsResponse2Correct;
        }

        public bool isAnswer2Correct()
        {
            return (response2Selected() == IsResponse2Correct);
        }
        public bool response2Selected()
        {
            if (isMultiChoice())
                return MCAnswer2.GetComponent<Toggle>().isOn;
            else
                return MSAnswer2.GetComponent<Toggle>().isOn;
        }
        public string getAnswer3()
        {
            return ResponseOption3.text;
        }
        public bool answer3Value()
        {
            return IsResponse3Correct;
        }
        public bool isAnswer3Correct()
        {
            return (response3Selected() == IsResponse3Correct);
        }
        public bool response3Selected()
        {
            if (isMultiChoice())
                return MCAnswer3.GetComponent<Toggle>().isOn;
            else
                return MSAnswer3.GetComponent<Toggle>().isOn;
        }
        public string getAnswer4()
        {
            return ResponseOption4.text;
        }
        public bool answer4Value()
        {
            return IsResponse4Correct;
        }
        public bool isAnswer4Correct()
        {
            return (response4Selected() == IsResponse4Correct);
        }
        public bool response4Selected()
        {
            if (isMultiChoice())
                return MCAnswer4.GetComponent<Toggle>().isOn;
            else
                return MSAnswer4.GetComponent<Toggle>().isOn;
        }

        public bool isMultiChoice()
        {
            if (QuestionType == questionType.MultiChoice)
                return true;
            else
                return false;
        }

        public void setQuestionSO(QuestionScriptableObject qSO)
        {
            questionSO = qSO;
        }
    }
}