using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTranslate : MonoBehaviour
{
	float t = 0.2f;

	void OnTriggerEnter(Collider isWall)
	{
		Debug.Log("Wall Detected! Ruuuun!");
		if(isWall.GetComponent<Collider>().tag == "Wall")
		{
			Vector3 newPos = this.transform.position + (Vector3.back * Time.deltaTime);

			while(Mathf.Abs((newPos - this.transform.position).sqrMagnitude) > 0.05f)
			{
				this.transform.position = Vector3.Lerp(this.transform.position, newPos, t);
			}

			this.transform.position = newPos;
		}
	}
}
