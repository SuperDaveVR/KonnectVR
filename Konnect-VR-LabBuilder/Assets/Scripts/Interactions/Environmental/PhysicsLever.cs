using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PhysicsLever : MonoBehaviour
{
    public float angle;
    /// <summary>
    /// Called when the lever is in the up position.
    /// </summary>
    public UnityEvent OnLeverUp;

    private bool leverUp = false;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnLeverDown;

    private bool leverDown = false;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnLeverNeutral;

    private bool leverNeutral = false;

    // Update is called once per frame
    void Update()
    {
        angle = transform.localEulerAngles.x;
        if (transform.localEulerAngles.x > 180 && transform.localEulerAngles.x < 320)
        {
            if (!leverUp)
            {
                leverUp = true;
                HandleLeverUp();
            }

            leverDown = false;
            leverNeutral = false;
        }
        else if (transform.localEulerAngles.x < 180 && transform.localEulerAngles.x > 40)
        {
            if (!leverDown)
            {
                leverDown = true;
                HandleLeverDown();
            }

            leverUp = false;
            leverNeutral = false;
        }
        else
        {
            if (!leverNeutral)
            {
                leverNeutral = true;
                HandleLeverNeutral();
            }

            leverUp = false;
            leverDown = false;
        }

    }

    private void HandleLeverUp()
    {
        OnLeverUp.Invoke();
    }

    private void HandleLeverDown()
    {
        OnLeverDown.Invoke();
    }

    private void HandleLeverNeutral()
    {
        OnLeverNeutral.Invoke();
    }
}
