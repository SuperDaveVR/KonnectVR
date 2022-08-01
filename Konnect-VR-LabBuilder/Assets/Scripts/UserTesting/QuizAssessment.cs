using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KonnectVR.UserTesting
{
    public class QuizAssessment : MonoBehaviour
    {
        public QuizAssessmentScriptableObject assessment;
        public GameObject QuestionPanelPrefab;
        public GameObject AnswerArea;
        public Assessment_UI_Button_Actions buttonActions;

        public List<GameObject> questions_in_assessment;

        public List<GameObject> getQuestions()
        {
            int i = 0;
            foreach (QuestionScriptableObject question in assessment.questions)
            {
                //var newQuestion = Instantiate(QuestionPanelPrefab);
                //newQuestion.transform.parent = AnswerArea.transform;
                var newQuestion = Instantiate(QuestionPanelPrefab, AnswerArea.transform);
                newQuestion.transform.localScale = Vector3.one;
                questions_in_assessment.Add(newQuestion);

                foreach (Toggle tog in newQuestion.GetComponentsInChildren<Toggle>())
                {
                    tog.onValueChanged.AddListener(buttonActions.answer_question);
                }
                Question q = newQuestion.GetComponent<Question>();

                q.setQuestionSO(question);
                q.setQuestionUI();
                if (i == 0)
                {
                    newQuestion.SetActive(true);
                }
                else
                {
                    newQuestion.SetActive(false);
                }
                i++;
            }
            return questions_in_assessment;
        }

        public string getQuizName()
        {
            return assessment.getQuizSOName();
        }
    }
}
