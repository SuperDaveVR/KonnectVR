using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverNegTrigger : MonoBehaviour
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
        if (other.tag == "Player" && tag == "NEG_Trigger")
        {
            GetComponentInParent<LeverTrigger>().NegTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Button has been released
        if (other.tag == "Player")
        {
            GetComponentInParent<LeverTrigger>().TriggerLeft = true;
        }
    }
}
