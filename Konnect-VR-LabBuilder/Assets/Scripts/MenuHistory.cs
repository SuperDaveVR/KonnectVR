using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHistory : MonoBehaviour
{
    private Stack<GameObject> history;
    
    private void Awake()
    {
        history = new Stack<GameObject>();
    }

    /// <summary>
    /// Returns the previous menu in the history.
    /// </summary>
    public void back()
    {
        if (history.Count > 1)
        {
            GameObject current = history.Pop(); //Remove current menu from history
            current.SetActive(false); //Hide current menu

            GameObject previous = history.Peek();
            previous.SetActive(true); //Show previous menu
        }
    }

    /// <summary>
    /// Adds an item to the history.
    /// </summary>
    /// <param name="item">Menu being added to the history.</param>
    public void add(GameObject item)
    {
        if (history.Count > 0)
            history.Peek().SetActive(false); //Hide current menu
        history.Push(item); //Add new menu to history
        item.SetActive(true);
    }
}
