using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public delegate void OnTrackpadPress(int deviceID, string side);
	public static OnTrackpadPress TrackpadPressed;

	public delegate void OnTriggerPress();
	public static OnTriggerPress TriggerPressed;
    
	private List<Rigidbody> keyRigidbodies = new List<Rigidbody>();

	void Start()
	{
		GameObject[] keys = GameObject.FindGameObjectsWithTag ("Key");
		for (int i = 0; i < keys.Length; i++)
		{
			keyRigidbodies.Add (keys [i].GetComponent<Rigidbody> ());
		}
	}

	void Update()
	{
		
	}

	void OnTriggerStay(Collider col)
	{
		
	}
}