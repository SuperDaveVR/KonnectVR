using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPosTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Button has been pressed
        if (other.tag == "Player" && tag == "POS_Trigger")
        {
            GetComponentInParent<SliderTrigger>().PosTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Button has been released
        if (other.tag == "Player")
        {
            GetComponentInParent<SliderTrigger>().TriggerLeft = true;
        }
    }
}
