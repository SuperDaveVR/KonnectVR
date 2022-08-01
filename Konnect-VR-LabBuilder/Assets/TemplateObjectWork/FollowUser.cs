using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUser : MonoBehaviour
{
    private Transform userTransform;
    // Start is called before the first frame update
    void Awake()
    {
        userTransform = GameObject.Find("XR Rig").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = userTransform.position;

        point.y = transform.position.y;
        transform.LookAt(point);

        Vector3 rotationToAdjust = (transform.rotation.eulerAngles);
        rotationToAdjust.y = adjustYValue(rotationToAdjust.y);

        transform.eulerAngles = rotationToAdjust;
    }

    private float adjustYValue(float value)
    {
        float multipleOf = 90;
        return Mathf.Round(value / multipleOf) * multipleOf;
    }
}
