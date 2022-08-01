using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

namespace KonnectVR.UserTesting
{
    
    [System.Serializable]
    public class Answer
    {
        [SerializeField]
        [TextArea(3, 20)]
        private string answerString;
        [SerializeField]
        private bool isCorrect;

        public string getAnswer () {
            return answerString;
        }

        public bool getIsCorrect () {
            return isCorrect;
        }
    }
}