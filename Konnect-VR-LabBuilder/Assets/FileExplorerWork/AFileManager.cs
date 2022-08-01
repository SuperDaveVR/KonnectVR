using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AFileManager : MonoBehaviour
{
    [SerializeField] protected SaveSerial saveSerial;

    public virtual void Load(string filePath)
    {
        Debug.Log("Loading File: " + filePath);
    }

    public virtual void Save(string filePath)
    {
        Debug.Log("Saving File: " + filePath);
    }

    public virtual string GetDefaultExtension()
    {
        string ext = ".dat";
        return ext;
    }
}
