using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Back : MonoBehaviour, IButton
{
    public int menuIndex;

    public void Clicked()
    {
        MenuManager.Instance.OpenMenu(menuIndex);
    }

}
