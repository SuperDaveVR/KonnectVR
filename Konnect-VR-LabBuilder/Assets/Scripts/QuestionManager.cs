using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{ 
    public GameObject identifyButton;
    public GameObject grabButton;
    
    public GameObject identifyUI;
    public GameObject grabUI;

    public GameObject identifyManager;
    public GameObject grabManager;

    public GameObject soundManager;
    public GameObject modeManager;

    public QuizIdentifyManager identifyScript;
    public TissueGrabManager grabScript;

    public bool isQuiz;

    private bool identifyDone = false;
    private bool grabDone = false;

    // Start is called before the first frame update
    void Start()
    {
       
        showAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAll()
    {
        modeManager.SetActive(true);
        soundManager.SetActive(false);

        if (isQuiz)
        {
            if (!identifyDone)
            {


                identifyButton.SetActive(true);
                identifyUI.SetActive(false);
                identifyManager.SetActive(false);
            }

            if (!grabDone)
            {


                grabManager.SetActive(false);
                grabUI.SetActive(false);
                grabButton.SetActive(true);
            }
           

        }
        else
        {
            identifyButton.SetActive(true);
            grabButton.SetActive(true);
          

            identifyUI.SetActive(false);
            grabUI.SetActive(false);
           

            identifyManager.SetActive(false);
            grabManager.SetActive(false);
           
        }

    }

    public void startIdentify()
    {
        modeManager.SetActive(false);
        soundManager.SetActive(true);

        identifyManager.SetActive(true);
        identifyButton.SetActive(false);
        grabButton.SetActive(false);
       

        identifyUI.SetActive(true);



        identifyDone = true;
        identifyScript.onStart();


    }

    public void startGrab()
    {
        modeManager.SetActive(false);
        soundManager.SetActive(true);

        identifyButton.SetActive(false);
        grabButton.SetActive(false);
       

        grabUI.SetActive(true);


        grabManager.SetActive(true);

        grabDone = true;
        grabScript.onStart();
    }


}
