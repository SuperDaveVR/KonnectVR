using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeModification : MonoBehaviour
{
    public int speed;
    public GameObject wheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left, 45 * Time.deltaTime * speed);
    }

    public void ChangeRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, wheel.transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void RotateClockwise()
    {
        speed = 5;
    }

    public void RotateCounterClockwise()
    {
        speed = -5;
    }

    public void RotateStop()
    {
        speed = 0;
    }

    public void SetCubeColor(Material color)
    {
        this.GetComponent<Renderer>().material = color;
    }

    public void setRotation(float angle)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
    }
}
