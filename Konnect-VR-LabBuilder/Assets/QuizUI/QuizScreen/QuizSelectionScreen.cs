using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSelectionScreen : TabletScreenHandler
{
    [SerializeField] private string quizListTag = "QuizList";
    [SerializeField] private GameObject ContentField;
    [SerializeField] private GameObject QuizButtonPrefab;
    [SerializeField] private string QuizScreen;
    [SerializeField] private GameObject AddQuizButton;
    private GameObject parentQuizList;

    // Start is called before the first frame update
    private void Awake()
    {
        if (parentQuizList == null)
        {
            parentQuizList = GameObject.FindGameObjectWithTag(quizListTag);
        }
        Reload();
    }

    void OnEnable()
    {
        Reload();
    }

    private void Reload()
    {
        Clear();
        LoadButtons();
    }

    private void LoadButtons()
    {
        GameObject newButton;
        Transform parentTransform = parentQuizList.transform;

        foreach (Transform t in parentTransform)
        {
            Debug.Log("Creating button for " + t.name);
            newButton = Instantiate(QuizButtonPrefab, ContentField.transform);

            newButton.GetComponent<QuizSelectButton>().BuildMe(t.gameObject, QuizScreen, this);

            if (t.GetComponent<QuizObj>() != null)
                newButton.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = t.GetComponent<QuizObj>().Name;
                
        }
        MoveAddToEnd(); //Moves the Add Quiz Button to the end
    }

    private void Clear()
    {
        Debug.Log("Deleting from " + ContentField.name);
        foreach (Transform child in ContentField.transform)
        {
            if (child != AddQuizButton.transform)
            {
                Debug.Log("Deleting " + child.name);
                Destroy(child.gameObject);
            }
        }
    }

    private void MoveAddToEnd()
    {
        AddQuizButton.transform.SetAsLastSibling();
    }

}
