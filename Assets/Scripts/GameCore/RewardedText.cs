using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardedText : MonoBehaviour
{
    public TMP_Text text;
    public AnimateTextColor animateTextColor;

    public void Animate(string str)
    {
        text.text = str;

        animateTextColor.Animate();
    }
}
