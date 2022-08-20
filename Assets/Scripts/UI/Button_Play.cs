using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Play : MonoBehaviour, IButton
{
    public int menuID;

    public void Clicked()
    {
        MenuManager.Instance.OpenMenu(menuID);
    }
}
