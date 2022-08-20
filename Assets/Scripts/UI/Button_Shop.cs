using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Shop : MonoBehaviour, IButton
{
    public int menuIndex;

    public void Clicked()
    {
        MenuManager.Instance.OpenMenu(menuIndex);
    }

}
