using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChildManager : MonoBehaviour
{
    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localScale;

        public TransformData(Transform targetTransform)
        {
            position = targetTransform.position;
            rotation = targetTransform.rotation;
            localScale = targetTransform.localScale;
        }
    }

    private Dictionary<Transform, TransformData> initialChildTransformData;

    // Start is called before the first frame update
    private void Start()
    {
        initialize();
    }

    private void initialize()
    {
        initialChildTransformData = new Dictionary<Transform, TransformData>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform) //Child is not the parent
            {
                initialChildTransformData.Add(child, new TransformData(child));
            }
        }
    }

    public void resetChildTransforms()
    {
        foreach (KeyValuePair<Transform, TransformData> childTransform in initialChildTransformData)
        {
            Transform child = childTransform.Key;
            TransformData transformData = childTransform.Value;

            child.position = transformData.position;
            child.rotation = transformData.rotation;
            child.localScale = transformData.localScale;

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb) //Reset rigidbody velocities if there is one attached
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
