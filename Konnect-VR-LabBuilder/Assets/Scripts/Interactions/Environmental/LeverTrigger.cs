using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    Animator m_Animator;

    public BoxCollider PositiveTrigger;
    public BoxCollider NegativeTrigger;

    //Modes
    public bool ReturnNeutral;
    public bool NeutralState;
    public bool DefaultDown;

    //Current State
    public bool Up;
    public bool Down;
    public bool Neutral;

    public bool PosTriggered;
    public bool NegTriggered;
    public bool TriggerLeft;

    /// <summary>
    /// Called when the lever is in the up position.
    /// </summary>
    public UnityEvent OnLeverUp;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnLeverDown;

    /// <summary>
    /// Called when the lever is in the down position.
    /// </summary>
    public UnityEvent OnLeverNeutral;

    // Start is called before the first frame update
    void Start()
    {
        
        m_Animator = GetComponent<Animator>();
        if (!NeutralState)
            m_Animator.SetBool("TwoState", true);
        Neutral = true;
        PositiveTrigger = transform.Find("Positive_Trigger").GetComponent<BoxCollider>();
        NegativeTrigger = transform.Find("Negative_Trigger").GetComponent<BoxCollider>();
        if(DefaultDown)
            MoveHandleDown();
    }

    // Update is called once per frame
    void Update()
    {
        if(PosTriggered)
        {
            MoveHandleDown();
            PosTriggered = false;
        }
        else if(NegTriggered)
        {
            MoveHandleUp();
            NegTriggered = false;
        }
        if (ReturnNeutral && TriggerLeft)
        {
            MoveHandleNeutral();
            TriggerLeft = false;
        }
        else if (TriggerLeft)
            TriggerLeft = false;
    }

    public void MoveHandleUp()
    {
        if (Down)
        {
            if (NeutralState)
            {
                m_Animator.SetTrigger("POS_Trigger");
                Down = false;
                Neutral = true;
                HandleLeverNeutral();
            }
            else
            {
                m_Animator.SetTrigger("Trigger");
                Down = false;
                Up = true;
                HandleLeverUp();
            }
        }
        else if(Neutral)
        {
            m_Animator.SetTrigger("POS_Trigger");
            Neutral = false;
            Up = true;
            HandleLeverUp();
        }
    }

    public void MoveHandleDown()
    {
        if (Up)
        {
            if (NeutralState)
            {
                m_Animator.SetTrigger("NEG_Trigger");
                Up = false;
                Neutral = true;
                HandleLeverNeutral();
            }
            else
            {
                m_Animator.SetTrigger("Trigger");
                Up = false;
                Down = true;
                HandleLeverDown();
            }
        }
        else if (Neutral)
        {
            m_Animator.SetTrigger("NEG_Trigger");
            Neutral = false;
            Down = true;
            HandleLeverDown();
        }
    }

    public void MoveHandleNeutral()
    {
        if(Up)
        {
            m_Animator.SetTrigger("NEG_Trigger");
            Up = false;
            Neutral = true;
            HandleLeverNeutral();
        }
        else if(Down)
        {
            m_Animator.SetTrigger("POS_Trigger");
            Down = false;
            Neutral = true;
            HandleLeverNeutral();
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
