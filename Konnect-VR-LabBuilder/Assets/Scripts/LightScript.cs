using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject RedLight;
    public GameObject GreenLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRed()
    {
        RedLight.SetActive(true);
        GreenLight.SetActive(false);
    }

    public void TurnGreen()
    {
        GreenLight.SetActive(true);
        RedLight.SetActive(false);
    }
}
