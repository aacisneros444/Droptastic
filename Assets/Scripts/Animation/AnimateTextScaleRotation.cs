using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimateTextScaleRotation : MonoBehaviour
{
    [Header("References")]
    public RectTransform rectTransform;

    [Header("Parameters")]
    public float duration;
    private float timeElapsed;
    public Vector3 vectorFrom;
    public Vector3 vectorTo;
    public Vector3 eulerAngles0;
    public Vector3 eulerAngles1;
    private byte angleIndex;

    private void Awake()
    {
        timeElapsed = duration;
    }

    private void Update()
    {
        if (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed < duration / 2f)
            {
                float interpValue = timeElapsed / (duration / 2);

                rectTransform.localScale = Vector3.Lerp(vectorFrom, vectorTo, interpValue);

                if(angleIndex == 0)
                {
                    rectTransform.eulerAngles = Vector3.Lerp(Vector3.zero, eulerAngles1, interpValue);
                }
                else
                {
                    rectTransform.eulerAngles = Vector3.Lerp(Vector3.zero, eulerAngles0, interpValue);

                }
            }
            else
            {
                float interpValue = ((timeElapsed - (duration / 2f)) / duration) * 2f;

                rectTransform.localScale = Vector3.Lerp(vectorTo, vectorFrom, interpValue);

                if (angleIndex == 0)
                {
                    rectTransform.eulerAngles = Vector3.Lerp(eulerAngles1, Vector3.zero, interpValue);
                }
                else
                {
                    rectTransform.eulerAngles = Vector3.Lerp(eulerAngles0, Vector3.zero, interpValue);

                }
            }
        }
        else
        {
            timeElapsed = 0f;

            if (angleIndex == 0)
            {
                angleIndex = 1;
            }
            else
            {
                angleIndex = 0;
            }
        }
    }
}
