using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayButtonPress()
    {
        ButtonFeebackManager.Instance.playButtonPressSound();
    }

    public void PlayBackButton()
    {
        ButtonFeebackManager.Instance.playBackButtonSound();
    }
}
