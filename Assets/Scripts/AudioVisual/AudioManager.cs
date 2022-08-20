using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("References")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    [Header("Combo Index")]
    [HideInInspector]public int comboIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudioClip(int index)
    {
        if(index  > 3)
        {
            if(index == 4)
            {
                comboIndex = 0;
                audioSource.PlayOneShot(audioClips[index], 1.0f);
                return;
            }

            audioSource.PlayOneShot(audioClips[index], 1.0f);
            return;
        }
        else
        {
            audioSource.PlayOneShot(audioClips[comboIndex], 1.0f);
            if(comboIndex < 3)
            {
                comboIndex++;
            }
        }
    }
}
