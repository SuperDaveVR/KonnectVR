using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimation : MonoBehaviour
{
    [SerializeField] private Animator myTriggerZone;
    [SerializeField] private Animator myInvisibleWall;
	//[SerializeField] private Animator myWater;
	//[SerializeField] private Animator mySaturatedSol;
	[SerializeField] private GameObject player;
	//[SerializeField] public GameObject water;
	//[SerializeField] public GameObject saturated;

    private bool hasEnteredAlready = false;

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Collision Detected!...");
        if(hasEnteredAlready) // Checks for second re-entry -- && other.GetComponent<Collider>().tag == "Player"
        {
            //Debug.Log("Player has reentered");
			myTriggerZone.SetBool("reEntered", true);
			myInvisibleWall.SetBool("startPushing", true);
			hasEnteredAlready = false;
			Debug.Log(hasEnteredAlready);
			//saturated.GetComponent<MeshRenderer>().enabled = true; // Turn the saturated solution mesh on
        } 
        else // Initial Entry
        {
            //Debug.Log("Player has entered");
			myTriggerZone.SetBool("isEntered", true);
			myInvisibleWall.SetBool("startPushing", true);
			//myWater.SetBool("hasEntered", true);
			hasEnteredAlready = true;
			//water.GetComponent<MeshRenderer>().enabled = true; // Turn the water mesh on
			Debug.Log("WATER ON");
			Debug.Log(hasEnteredAlready);
        }

		//water.GetComponent<MeshRenderer>().enabled = true; // Turn the water mesh on
    }

    private void OnTriggerExit(Collider other)
    {
		//Debug.Log("Bye Bye...");
        if(hasEnteredAlready) // Initial Exit -- && other.GetComponent<Collider>().tag == "Player"
        {
            //Debug.Log("Player has exited");
			myTriggerZone.SetBool("isEntered", false);
			myInvisibleWall.SetBool("startPushing", false);
			//myWater.SetBool("hasEntered", false);
			//mySaturatedSol.SetBool("reEntered", true);
			//water.GetComponent<MeshRenderer>().enabled = false; // Turn the water mesh off
			//saturated.GetComponent<MeshRenderer>().enabled = true; // Turn the saturated solution mesh on
			Debug.Log("SATURATED ON");
			Debug.Log("WATER OFF");
        }
        else // Checks for second exit
        {
            //Debug.Log("Player has reexitted");
			if(other.GetComponent<Collider>().tag == "Player")
			{
				myTriggerZone.SetBool("reEntered", false);
				myInvisibleWall.SetBool("startPushing", false);
				//mySaturatedSol.SetBool("reEntered", false);
				//saturated.GetComponent<MeshRenderer>().enabled = false; // Turn the saturated solution mesh off
				Debug.Log("SATURATED OFF");
			}
        }

		//water.GetComponent<MeshRenderer>().enabled = false; // Turn the water mesh off
    }

	void Start(){
		//Debug.Log("Script is working...");
	}
}
