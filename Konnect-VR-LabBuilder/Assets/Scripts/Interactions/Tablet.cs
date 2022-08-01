using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Tablet : MonoBehaviour
{
    [Header("Rig")]
    public GameObject rig;
    public GameObject leftSocket;
    public GameObject rightSocket;

    [Header("Resize")]
    public GameObject ShrinkButton;
    public GameObject EnlargeButton;

    [Header("Follow")]
    public GameObject FollowButton;
    public GameObject UnfollowButton;
    
    [Header("Mute/Unmute")]
    public GameObject MuteButton;
    public GameObject UnmuteButton;

    [Header("Date and Time")]
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;
    private DateTime dateTime;

    private bool leftSocketDisabled = false;
    private bool rightSocketDisabled = false;
    private bool positionCheck = false;
    private bool inLeft = false;
    private bool inRight = false;

    [SerializeField] private float smallScaleDefault = 1.42008f;
    [SerializeField] private float largeScaleDefault = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dateTime = DateTime.Now;
        dateText.text = dateTime.ToShortDateString();
        timeText.text = dateTime.ToShortTimeString();
        if(positionCheck)
        {
            if (inLeft)
            {
                transform.position = leftSocket.transform.position;
                inRight = false;
            }
            if (inRight)
            {
                transform.position = rightSocket.transform.position;
                inRight = false;
            }
            positionCheck = false;
        }
        if(leftSocketDisabled)
        {
            leftSocket.SetActive(true);
            leftSocketDisabled = false;
            positionCheck = true;
        }
        if(rightSocketDisabled)
        {
            rightSocket.SetActive(true);
            rightSocketDisabled = false;
            positionCheck = true;
        }
    }

    public void TeleportLeftSocket()
    {
        rightSocket.SetActive(false);
        transform.position = leftSocket.transform.position;
        rightSocketDisabled = true;
        inLeft = true;
    }

    public void TeleportRightSocket()
    {
        leftSocket.SetActive(false);
        transform.position = rightSocket.transform.position;
        leftSocketDisabled = true;
        inRight = true;
    }

    public void Shrink()
    {
        gameObject.transform.localScale = new Vector3(smallScaleDefault, smallScaleDefault, smallScaleDefault);
        ShrinkButton.SetActive(false);
        EnlargeButton.SetActive(true);
    }

    public void Enlarge()
    {
        gameObject.transform.localScale = new Vector3(largeScaleDefault, largeScaleDefault, largeScaleDefault);
        ShrinkButton.SetActive(true);
        EnlargeButton.SetActive(false);
    }

    public void Follow()
    {
        gameObject.transform.SetParent(rig.transform);
        FollowButton.SetActive(false);
        UnfollowButton.SetActive(true);
    }

    public void Unfollow()
    {
        gameObject.transform.SetParent(null);
        FollowButton.SetActive(true);
        UnfollowButton.SetActive(false);
    }

    public void Mute()
    {
        //logic for muting needed
        MuteButton.SetActive(false);
        UnmuteButton.SetActive(true);
    }

    public void Unmute()
    {
        //logic for unmuting needed
        MuteButton.SetActive(true);
        UnmuteButton.SetActive(false);
    }
}
