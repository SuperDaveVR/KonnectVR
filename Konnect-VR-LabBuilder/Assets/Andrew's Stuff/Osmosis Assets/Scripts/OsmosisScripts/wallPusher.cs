using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallPusher : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		float force = 10000;

		if(other.GetComponent<Collider>().tag == "Wall")
		{
			Debug.Log("Wall Detected!");
			Vector3 dir = transform.position;
			dir = -dir.normalized;
			GetComponent<Rigidbody>().AddForce(dir * force);
		}
	}
}
