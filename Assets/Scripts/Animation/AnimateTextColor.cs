using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateTextColor : MonoBehaviour
{
    [Header("References")]
    public TMP_Text text;

    [Header("Parameters")]
    public float duration;
    private float timeElapsed;
    public Color colorFrom;
    public Color colorTo;

    private void Awake()
    {
        timeElapsed = duration;
    }

    public void Animate()
    {
        timeElapsed = 0f;
    }

    private void Update()
    {
        if(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed < duration / 2f)
            {
                text.color = Color.Lerp(colorFrom, colorTo, ((timeElapsed + (duration / 2f)) / duration));
            }
            else
            {
                text.color = Color.Lerp(colorTo, colorFrom, ((timeElapsed - (duration / 2f)) / duration) * 2f);
            }
        }
    }
}
