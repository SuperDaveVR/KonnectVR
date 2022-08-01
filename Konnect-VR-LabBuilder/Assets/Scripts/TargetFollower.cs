using UnityEngine;

[ExecuteAlways]
public class TargetFollower : MonoBehaviour
{
    [SerializeField, Tooltip("Transform to follow.")]
    private Transform target;

    [Header("Translation")]

    [SerializeField]
    private bool matchPositionX = true;

    [SerializeField]
    private bool matchPositionY = true;

    [SerializeField]
    private bool matchPositionZ = true;

    [SerializeField, Tooltip("Determines if the position should be constrained to movement only on the selected axes.")]
    private bool constrainPosition = false;

    [Tooltip("Speed at which the target will be followed if matchPosition is checked.")]
    [Range(0f, 1f), SerializeField]
    private float translationSpeed = 1f;

    [Header("Rotation")]

    [SerializeField]
    private bool matchRotationX = true;

    [SerializeField]
    private bool matchRotationY = true;

    [SerializeField]
    private bool matchRotationZ = true;

    [SerializeField, Tooltip("Determines if the position should be constrained to movement only on the selected axes.")]
    private bool constrainRotation = false;

    [Tooltip("Speed at which the rotation of the target will be matched if matchRotation is checked.")]
    [Range(0f, 1f), SerializeField]
    private float rotationSpeed = 0.5f;

    /// <summary>
    /// Follow target position on the given axes.
    /// </summary>
    private void followPosition()
    {
        if (target && (matchPositionX || matchPositionY || matchPositionZ))
        {
            Vector3 position = transform.position;
            Vector3 targetPosition = target.position;
            Vector3 lerpPosition = Vector3.Lerp(position, targetPosition, translationSpeed);
            float x = matchPositionX ? lerpPosition.x : constrainPosition ? 0f : position.x;
            float y = matchPositionY ? lerpPosition.y : constrainPosition ? 0f : position.y;
            float z = matchPositionZ ? lerpPosition.z : constrainPosition ? 0f : position.z;
            transform.position = new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// Follow target rotation on the selected axes.
    /// </summary>
    private void followRotation()
    {
        if (target && (matchRotationX || matchRotationY || matchRotationZ))
        {
            Vector3 rawTargetRotationEuler = target.eulerAngles;
            Vector3 currentRotationEuler = transform.eulerAngles;
            float x = matchRotationX ? rawTargetRotationEuler.x : constrainRotation ? 0f : currentRotationEuler.x;
            float y = matchRotationY ? rawTargetRotationEuler.y : constrainRotation ? 0f : currentRotationEuler.y;
            float z = matchRotationZ ? rawTargetRotationEuler.z : constrainRotation ? 0f : currentRotationEuler.z;
            Quaternion targetRotation = Quaternion.Euler(x, y, z);
            Quaternion lerpRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
            transform.rotation = lerpRotation;
        }
    }

    ///// <summary>
    ///// Sets the transform position to the target position.
    ///// </summary>
    //public void snapToTargetPosition()
    //{
    //    //Move to target
    //    if (target)        
    //        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    //}

    ///// <summary>
    ///// Sets the transform rotation to the target rotation with rotation offset.
    ///// </summary>
    //public void snapToTargetRotation()
    //{
    //    //Rotate to target
    //    if (target)
    //    {
    //        Vector3 rawTargetRotationEuler = target.eulerAngles;
    //        Vector3 currentRotationEuler = transform.eulerAngles;
    //        float x = matchRotationX ? rawTargetRotationEuler.x : currentRotationEuler.x;
    //        float y = matchRotationY ? rawTargetRotationEuler.y : currentRotationEuler.y;
    //        float z = matchRotationZ ? rawTargetRotationEuler.z : currentRotationEuler.z;
    //        Quaternion targetRotation = Quaternion.Euler(x, y, z);
    //        transform.rotation = targetRotation;
    //    }
    //}
    
    private void Update()
    {
        followPosition();
        followRotation();
    }
}