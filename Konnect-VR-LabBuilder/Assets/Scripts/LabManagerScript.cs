using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabManagerScript : MonoBehaviour
{
  
    public GameObject learningModeUI;
    public GameObject quizModeUI;
    public GameObject practiceModeUI;
    public GameObject learningModeObj;
    public GameObject quizModeObj;
    public GameObject practiceModeObj;
    public Button learningModeButton;
    public Button practiceModeButton;
    public Button quizModeButton;

    public TextMeshProUGUI ModeText;



    // Start is called before the first frame update
    void Start()
    {
        EnterLearningMode();
        //ModeText = GameObject.Find("ModeDescription").GetComponent<TextMeshProUGUI>();




    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterLearningMode()
    {
        //Enable learning mode skeleton and ui, disable others
        
        learningModeUI.SetActive(true);
        quizModeUI.SetActive(false);
        practiceModeUI.SetActive(false);
        learningModeObj.SetActive(true);
        quizModeObj.SetActive(false);
        practiceModeObj.SetActive(false);
        ModeText.text = "Current Mode: Learning Mode";


    }

    public void EnterPracticeMode()
    {
        //Enable practice mode skeleton and ui, disable others
        
        learningModeUI.SetActive(false);
        quizModeUI.SetActive(false);
        practiceModeUI.SetActive(true);
        learningModeObj.SetActive(false);
        quizModeObj.SetActive(false);
        practiceModeObj.SetActive(true);
        ModeText.text = "Current Mode: Practice Mode";



    }

    public void EnterQuizMode()
    {

        //Enable quiz mode skeleton and ui, disable others
        
        learningModeUI.SetActive(false);
        practiceModeUI.SetActive(false);
        quizModeUI.SetActive(true);
        learningModeObj.SetActive(false);
        quizModeObj.SetActive(true);
        practiceModeObj.SetActive(false);
        ModeText.text = "Current Mode: Quiz Mode";



    }

}
