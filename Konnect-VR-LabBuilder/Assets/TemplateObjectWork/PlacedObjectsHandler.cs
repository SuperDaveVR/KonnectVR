using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacedObjectsHandler : MonoBehaviour
{
    public static PlacedObjectsHandler Instance;

    [SerializeField] private List<GameObject> PlacedObjects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        BuildList();
    }

    public void BuildList()
    {
        PlacedObjects.Clear();

        foreach (Transform childObject in this.transform)
        {
            PlacedObjects.Add(childObject.gameObject);
            Debug.Log(childObject.name);
        }
    }

    public void AddObject(GameObject obj)
    {
        PlacedObjects.Add(obj);
    }

    public void RemoveObject(int index)
    {
        GameObject ObjectToRemove = PlacedObjects[index];
        PlacedObjects.Remove(ObjectToRemove);
        Destroy(ObjectToRemove);
    }

    public Vector3 GetPosition(int index)
    {
        GameObject PositionObject = PlacedObjects[index];
        Vector3 Position = PositionObject.GetComponent<GetSetRotationAndPosition>().getPosition();

        return Position;
    }

    public void SetPosition(int index, Vector3 newPosition)
    {
        GameObject PositionObject = PlacedObjects[index];
        PositionObject.GetComponent<GetSetRotationAndPosition>().setPosition(newPosition);
    }

    public Quaternion GetRotation(int index)
    {
        GameObject PositionObject = PlacedObjects[index];
        Quaternion Rotation = PositionObject.GetComponent<GetSetRotationAndPosition>().getRotation();

        return Rotation;
    }

    public void SetRotation(int index, Quaternion newRotation)
    {
        GameObject PositionObject = PlacedObjects[index];
        PositionObject.GetComponent<GetSetRotationAndPosition>().setRotation(newRotation);
    }

    public Vector3 GetScale(int index)
    {
        GameObject PositionObject = PlacedObjects[index];
        Vector3 Scale = PositionObject.GetComponent<GetSetRotationAndPosition>().getScale();

        return Scale;
    }

    public void SetScale(int index, Vector3 newScale)
    {
        GameObject PositionObject = PlacedObjects[index];
        PositionObject.GetComponent<GetSetRotationAndPosition>().setScale(newScale);
    }

    public string GetName(int index)
    {
        GameObject PositionObject = PlacedObjects[index];
        return PositionObject.name;
    }

    public bool CheckObjectExists(int index)
    {
        bool doesExist = true;
        GameObject SearchObject = PlacedObjects[index];
        if (SearchObject == null)
        {
            doesExist = false;
        }

        return doesExist;
    }

    public bool CheckObjectExists(string name)
    {
        bool doesExist = true;
        GameObject SearchObject = PlacedObjects.Where(x => x.name == name).SingleOrDefault();
        if (SearchObject == null)
        {
            doesExist = false;
        }

        return doesExist;
    }

    public int listSize()
    {
        int size = PlacedObjects.Count;

        return size;
    }

    public GameObject GetObject(int index)
    {
        return PlacedObjects[index];
    }
}
