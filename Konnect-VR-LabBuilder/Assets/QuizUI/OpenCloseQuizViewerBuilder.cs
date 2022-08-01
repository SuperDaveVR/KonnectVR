using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
    public GameObject Panel;
    bool active;

    public void OpenAndClosePanel()
    {
        if (active == false)
        {
            Panel.SetActive(true);
            active = true;
        }

        else
        {
            Panel.SetActive(false);
            active = false;
        }
    }
}