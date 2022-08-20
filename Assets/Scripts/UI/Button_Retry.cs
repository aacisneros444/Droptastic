using UnityEngine;

public class Button_Retry : MonoBehaviour, IButton
{
    public void Clicked()
    {
        GameLevelManager.Instance.LoadCurrentLevel();
        MenuManager.Instance.CloseAllMenus();
    }
}
