using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class PhysicsWheel : MonoBehaviour
{
    public float minAngle;
    public float maxAngle;
    private float currentAngle;
    private float previousAngle;
    private float rotations;
    public float totalAngle;
    private Vector3 CorrectedAngles;
    /// <summary>
    /// Called when the lever is in the up position.
    /// </summary>
    public UnityEvent OnWheelMax;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnWheelMin;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public FloatEvent OnRotationChange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        previousAngle = currentAngle;
        currentAngle = this.transform.eulerAngles.y;
        if (currentAngle - previousAngle > 300)
            rotations--;
        else if (currentAngle - previousAngle < -300)
            rotations++;
        totalAngle = currentAngle + 360 * rotations;
        BoundaryCheck();
        if (currentAngle != previousAngle)
            HandleRotationChange();
    }

    //checks to see if the wheel has been turned to its max or min
    private void BoundaryCheck()
    {
        if (totalAngle > maxAngle)
        {
            CorrectedAngles = new Vector3(transform.localEulerAngles.x, maxAngle % 360, transform.localEulerAngles.z);
            transform.localEulerAngles = CorrectedAngles;
            HandleWheelMax();
        }
        else if(totalAngle < minAngle)
        {
            CorrectedAngles = new Vector3(transform.localEulerAngles.x, minAngle % 360, transform.localEulerAngles.z);
            transform.localEulerAngles = CorrectedAngles;
            HandleWheelMin();
        }
    }

    private void HandleWheelMax()
    {
        OnWheelMax.Invoke();
    }
    private void HandleWheelMin()
    {
        OnWheelMin.Invoke();
    }
    private void HandleRotationChange()
    {
        OnRotationChange.Invoke(currentAngle);
    }
}
