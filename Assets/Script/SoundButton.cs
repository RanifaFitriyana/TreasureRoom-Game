using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    public Image buttonImage;

    private void Start()
    {
        UpdateIcon();
    }

    public void ToggleSound()
    {
        MusicManager.Instance.ToggleMute();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (MusicManager.Instance.IsMuted())
            buttonImage.sprite = soundOff;
        else
            buttonImage.sprite = soundOn;
    }
}