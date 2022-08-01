using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPhysics : MonoBehaviour
{
    public float smoothingAmount = 15.0f;
    public Transform target = null;

    private Rigidbody rigidbody = null;
    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TeleportToTarget();
    }

    private void Update()
    {
        SetTargetPosition();
        SetTargetRotation();
    }

    private void SetTargetPosition()
    {
        float time = smoothingAmount * Time.unscaledDeltaTime;
        targetPosition = Vector3.Lerp(targetPosition, target.position, time);
    }

    private void SetTargetRotation()
    {
        float time = smoothingAmount * Time.unscaledDeltaTime;
        targetRotation = Quaternion.Slerp(targetRotation, target.rotation, time);
    }

    private void FixedUpdate()
    {
        MoveToController();
        RotateToController();
    }

    private void MoveToController()
    {
        Vector3 positionDelta = targetPosition - transform.position;

        rigidbody.velocity = Vector3.zero;
        rigidbody.MovePosition(transform.position + positionDelta);
    }

    private void RotateToController()
    {
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.MoveRotation(targetRotation);
    }

    public void TeleportToTarget()
    {
        targetPosition = target.position;
        targetRotation = target.rotation;

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
