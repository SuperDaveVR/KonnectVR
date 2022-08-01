using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFeedbackController : MonoBehaviour
{
	private const int KeyPressFeedbackStrength = 1500;
	private bool isColliding = false;

	void Start()
	{
		Key.keyPressed += KeyPressedHapticFeedback;
	}

	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
	}

	private void KeyPressedHapticFeedback()
	{
		if (isColliding)
		{
			StartCoroutine ("TriggerHapticFeedback", KeyPressFeedbackStrength);
		}
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		Key.keyPressed -= KeyPressedHapticFeedback;
	}

	private IEnumerator TriggerHapticFeedback(int strength)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
	}
}