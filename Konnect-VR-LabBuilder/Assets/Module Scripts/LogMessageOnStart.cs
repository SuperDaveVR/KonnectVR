using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.ModuleScripts
{
    public class LogMessageOnStart : MonoBehaviour
    {
        public string message = "Hello world!";

        private void Start()
        {
            Debug.Log(message);
        }
    }
}