using UnityEngine;

namespace KonnectVR.UserTesting
{
    public enum questionType
    {
        MultiChoice,
        MulitSelect
    }
    [CreateAssetMenu(fileName = "New Question", menuName = "Question")]
    public class QuestionScriptableObject : ScriptableObject
    {
        public string QuestionName;
        public questionType type;

        [TextArea(7,20)]
        public string QuestionText;

        public Answer answer1;
        public Answer answer2;
        public Answer answer3;
        public Answer answer4;

    }
}