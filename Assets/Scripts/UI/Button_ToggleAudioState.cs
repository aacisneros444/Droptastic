using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_ToggleAudioState : MonoBehaviour, IButton
{
    //value of 0 means off
    //value of 1 means on

    [Header("References")]
    public Image soundOnIcon;
    public Image soundOffIcon;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Audio State"))
        {
            PlayerPrefs.SetInt("Audio State", 1);
            PlayerPrefs.Save();
        }

        if(CheckAudioState() == 0)
        {
            SoundOff();
            return;
        }

        if(CheckAudioState() == 1)
        {
            SoundOn();
        }
    }

    public void Clicked()
    {
        if(CheckAudioState() == 1)
        {
            PlayerPrefs.SetInt("Audio State", 0);
            PlayerPrefs.Save();
            SoundOff();
            return;
        }

        if (CheckAudioState() == 0)
        {
            PlayerPrefs.SetInt("Audio State", 1);
            PlayerPrefs.Save();
            SoundOn();
        }
    }

    private int CheckAudioState()
    {
        if(PlayerPrefs.GetInt("Audio State") == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void SoundOn()
    {
        AudioListener.volume = 1f;
        ChangeUIIcon(1);
    }

    private void SoundOff()
    {
        AudioListener.volume = 0f;
        ChangeUIIcon(0);
    }

    private void ChangeUIIcon(int state)
    {
        if(state == 0)
        {
            soundOnIcon.gameObject.SetActive(false);
            soundOffIcon.gameObject.SetActive(true);
            return;
        }

        if (state == 1)
        {
            soundOnIcon.gameObject.SetActive(true);
            soundOffIcon.gameObject.SetActive(false);
        }
    }
}
