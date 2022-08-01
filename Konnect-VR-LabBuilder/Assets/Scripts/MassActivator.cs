using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassActivator : MonoBehaviour
{
    public List<GameObject> Objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChildren(GameObject Parent)
    {
        foreach (Transform child in Parent.transform)
        {
            AddToList(child.gameObject);
        }

    }

    public void AddToList(GameObject UIElement)
    {
        Objects.Add(UIElement);
    }

    public void ObjectActivator()
    {
        foreach (GameObject UIElement in Objects)
        {
            UIElement.SetActive(true);
        }
    }

    public void ObjectDisabler()
    {
        foreach (GameObject UIElement in Objects)
        {
            UIElement.SetActive(false);
        }
    }
}
