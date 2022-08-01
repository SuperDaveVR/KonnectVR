using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeletion : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToDestroy;

    public void Destroy()
    {
        Destroy(ObjectToDestroy);
    }
}
