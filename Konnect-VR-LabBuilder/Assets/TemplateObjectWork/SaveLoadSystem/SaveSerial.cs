using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using KonnectVR.Interactions;
using Unity.Netcode;

public class SaveSerial : MonoBehaviour
{
    //Lab Object Variables
    [SerializeField] private string placedObjectsParentName = "PlacedObjects";
    [SerializeField] private string prefabFilePath = "Prefabs/TemplateLabPlacementObject";
    [SerializeField] private string labObjectFilePath = "Prefabs/LabInteractableObject";
    [SerializeField] private string networkObjectFilePath = "Prefabs/NetworkLabInteractableObject";
    public bool canEdit = true;
    public List<PlacedObjectSaveData> placedObjectSaveDatas;

    //QuizObj Variables
    [SerializeField] private GameObject quizObjPrefab;
    [SerializeField] private string quizSystemTag = "QuizList";
    private GameObject quizSystem;

    private void Start()
    {
        quizSystem = GameObject.FindGameObjectWithTag(quizSystemTag);
    }


    public void SaveLab(string filePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        SaveData data = new SaveData();
        data.placedObjectSaveDatas = RetrieveObjectData();
        data.quizObjSaveData = RetrieveQuizzes();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Lab Data is Saved to " + filePath);
    }

    //Generates the list of placed object Data
    private List<PlacedObjectSaveData> RetrieveObjectData()
    {
        List<PlacedObjectSaveData> objectsData = new List<PlacedObjectSaveData>();
        GameObject placedObjectHolder = GameObject.Find(placedObjectsParentName);

        //Return each saveable child object
        foreach (Transform child in placedObjectHolder.transform)
        {
            GameObject childObject = child.gameObject;
            SaveablePlacedObject saveable = childObject.GetComponent<SaveablePlacedObject>(); 

            if (saveable != null)
            {
                PlacedObjectSaveData dataToSave = saveable.GetSaveableData();
                objectsData.Add(dataToSave);
            }
        }

        return objectsData;
    }

    //Generates the list of quiz data
    private List<QuizSaveData> RetrieveQuizzes()
    {
        List<QuizSaveData> quizData = new List<QuizSaveData>();

        QuizObj[] quizObjArray = quizSystem.GetComponentsInChildren<QuizObj>();

        foreach (QuizObj quizObj in quizObjArray)
        {
            quizData.Add(quizObj.GetSaveData());
        }

        return quizData;
    }

    public void LoadLab(string filePath)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            LoadObjectData(data.placedObjectSaveDatas);
            LoadQuizData(data.quizObjSaveData, true);
            Debug.Log("Lab " + filePath + " has been loaded");
        } else
        {
            Debug.Log("File does not exist");
        }
    }

    public void LoadQuizzesOnly(string filePath)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            LoadQuizData(data.quizObjSaveData, false);
            Debug.Log("Lab " + filePath + " has been loaded");
        }
        else
        {
            Debug.Log("File does not exist");
        }
    }

    private void LoadObjectData(List<PlacedObjectSaveData> objectDatas)
    {
        GameObject parentObject = GameObject.Find(placedObjectsParentName);

        //Clear out old children
        ClearChildren(parentObject);

        if (NetworkManager.Singleton.IsListening) {
            MultiLoadObjectData(objectDatas, parentObject);
        } else
        {
            SingleLoadObjectData(objectDatas, parentObject);
        }

        PlacedObjectsHandler placedObjectsHandler = parentObject.GetComponent<PlacedObjectsHandler>();

        if (placedObjectsHandler != null)
        {
            placedObjectsHandler.BuildList();
        }
    }

    private void SingleLoadObjectData(List<PlacedObjectSaveData> objectDatas, GameObject parentObject)
    {
        GameObject PrefabObject;

        foreach (PlacedObjectSaveData objectData in objectDatas)
        {
            if (canEdit)
            {
                PrefabObject = (GameObject)Instantiate(Resources.Load(prefabFilePath), parentObject.transform);
            }
            else
            {
                PrefabObject = (GameObject)Instantiate(Resources.Load(labObjectFilePath), parentObject.transform);
            }
            PrefabObject.GetComponent<SaveablePlacedObject>().LoadData(objectData);
        }
    }

    private void MultiLoadObjectData(List<PlacedObjectSaveData> objectDatas, GameObject parentObject)
    {
        GameObject PrefabObject;

        foreach (PlacedObjectSaveData objectData in objectDatas)
        {
            PrefabObject = (GameObject)Instantiate(Resources.Load(networkObjectFilePath), parentObject.transform);
            PrefabObject.GetComponent<SaveablePlacedObject>().LoadData(objectData);
        }
    }

    private void LoadQuizData(List<QuizSaveData> quizObjs, bool clearData)
    {
        if (clearData)
            ClearChildren(quizSystem);

        GameObject quizPrefab;

        foreach (QuizSaveData quizObj in quizObjs)
        {
            quizPrefab = (GameObject)Instantiate(quizObjPrefab, quizSystem.transform);
            quizPrefab.GetComponent<QuizObj>().LoadFromSaveData(quizObj);
        }
    }

    private void ClearChildren(GameObject parentObject)
    {
        if (parentObject)
        {
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
