using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeebackManager : MonoBehaviour
{
    private static ButtonFeebackManager instance;
    public static ButtonFeebackManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<ButtonFeebackManager>();
            return instance;
        }
    }

    [SerializeField]
    private AudioSource buttonPressSound;

    [SerializeField]
    private AudioSource backButtonSound;

    public void playButtonPressSound()
    {
        buttonPressSound.Play();
    }

    public void playBackButtonSound()
    {
        backButtonSound.Play();
    }
}
