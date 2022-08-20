using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_TikTok : MonoBehaviour, IButton
{
    public void Clicked()
    {
        Application.OpenURL("https://www.tiktok.com/@alexmakesgames");
    }
}
