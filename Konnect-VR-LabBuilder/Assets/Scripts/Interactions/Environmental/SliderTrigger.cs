using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class SliderEvent : UnityEvent<int>
{
}

public class SliderTrigger : MonoBehaviour
{
    Animator m_Animator;

    public bool PosTriggered = false; //Trigger on positive side of lever was triggered
    public bool NegTriggered = false; //Trigger on negative side of lever was triggered
    public bool TriggerLeft = false; //A trigger was left

    public int CurrentPos = 1; //current position of the slider
    private int MaxPos = 5; //maximum position of the slider
    private int MinPos = 1; //minimum position of the slider

    //Event for when the slider's position changes
    public SliderEvent OnSliderPositionChange;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PosTriggered && CurrentPos < MaxPos)
        {
            CurrentPos++;
            m_Animator.SetTrigger("POS_Trigger");
            OnSliderPositionChange.Invoke(CurrentPos);
            PosTriggered = false;
        }
        else if (NegTriggered && CurrentPos > MinPos)
        {
            CurrentPos--;
            m_Animator.SetTrigger("NEG_Trigger");
            OnSliderPositionChange.Invoke(CurrentPos);
            NegTriggered = false;
        }
    }
}
