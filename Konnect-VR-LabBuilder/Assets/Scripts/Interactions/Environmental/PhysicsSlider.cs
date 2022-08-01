using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PhysicsSlider : MonoBehaviour
{
    private float SliderMin = -0.098f;
    private float SliderMax = 0.098f;
    public float CurrentPos;

    /// <summary>
    /// Called when the lever is in the up position.
    /// </summary>
    public UnityEvent OnSliderMax;

    private bool sliderMax = false;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnSliderlMin;

    private bool sliderMin = false;

    // Update is called once per frame
    void Update()
    {
        BoundaryCheck();
    }

    private void BoundaryCheck()
    {
        CurrentPos = transform.localPosition.z;
        if (transform.localPosition.z < SliderMin)
        {
            if (!sliderMin)
            {
                sliderMin = true;

                
                HandleSliderMin();
            }
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, SliderMin);
            sliderMax = false;
        }
        else if(transform.localPosition.z > SliderMax)
        {
            if (!sliderMax)
            {
                sliderMax = true;

                
                HandleSliderMax();
            }
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, SliderMax);
            sliderMin = false;
        }
            
    }

    private void HandleSliderMin()
    {
        OnSliderlMin.Invoke();
    }

    private void HandleSliderMax()
    {
        OnSliderMax.Invoke();
    }
}
