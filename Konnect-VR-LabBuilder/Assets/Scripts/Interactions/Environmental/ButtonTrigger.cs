using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public bool Toggle;
    private bool ButtonTriggered;
    private bool Toggled;

    Animator m_Animator;
    /// <summary>
    /// Called when the button reaches the pressed state.
    /// </summary>
    public UnityEvent OnButtonPress;

    /// <summary>
    /// Called when the button leaves the pressed state.
    /// </summary>
    public UnityEvent OnButtonRelease;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        Toggled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Button has been pressed
        if (other.tag == "Player")
        {
            if(ButtonTriggered == false)
                handleButtonPress();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Button has been released
        if (other.tag == "Player")
        {
            if(ButtonTriggered == true)
                handleButtonRelease();
        }
    }

    private void handleButtonPress()
    {
        ButtonTriggered = true;
        if(Toggle == false)
        {
            m_Animator.SetTrigger("TriggerEnter");
            OnButtonPress.Invoke();
        }
        else
        {
            if(Toggled == false)
            {
                m_Animator.SetTrigger("TriggerEnter");
                OnButtonPress.Invoke();
                Toggled = true;
            }
            else
            {
                m_Animator.SetTrigger("TriggerExit");
                OnButtonRelease.Invoke();
                Toggled = false;
            }
        }
        
    }

    private void handleButtonRelease()
    {
        ButtonTriggered = false;
        if(Toggle == false)
        {
            m_Animator.SetTrigger("TriggerExit");
            OnButtonRelease.Invoke();
        }
    }
}
