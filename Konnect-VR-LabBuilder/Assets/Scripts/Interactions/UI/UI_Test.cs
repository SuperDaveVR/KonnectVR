using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Test : MonoBehaviour
{
    int counter;
    public Text counterText;
    
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void incrementCounter()
    {
        counter++;
        counterText.text = counter.ToString();
    }
}
