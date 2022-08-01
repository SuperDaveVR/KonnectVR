using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullButton : MonoBehaviour
{
    public GameObject room;
    public GameObject lights;
    public GameObject skull;

    public void toggleButtonOff()
    {
        if (room.activeSelf == true && lights.activeSelf == true && skull.activeSelf == true)
        {
            Debug.Log("Everthing is active");
            toggleOff();
        }
        else if(room.activeSelf == false&& lights.activeSelf == false&& skull.activeSelf == false)
        {
            Debug.Log("Everthing is inactive");
            toggleOn();
        }
    }

    private void toggleOn()
    {
        room.SetActive(true);
        lights.SetActive(true);
        skull.SetActive(true);
    }

    private void toggleOff()
    {
        room.SetActive(false);
        lights.SetActive(false);
        skull.SetActive(false);
    }
}
