using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.UserTesting
{
    [CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz")]
    public class QuizAssessmentScriptableObject : ScriptableObject
    {
        public string QuizName;

        public List<QuestionScriptableObject> questions;

        //This method is not used in the current version of this platform
        public void addQuestion(QuestionScriptableObject question)
        {
            int i = 0;
            foreach(QuestionScriptableObject ques in questions)
            {
                if (ques == question)
                {
                    break;
                }
                if (i == (questions.Count - 1))
                    questions.Add(question);
                i++;
            }
        }

        public string getQuizSOName()
        {
            return QuizName;
        }
    }
}
