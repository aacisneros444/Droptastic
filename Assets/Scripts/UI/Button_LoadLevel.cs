using UnityEngine;

public class Button_LoadLevel : MonoBehaviour, IButton
{
    public int gameMode;
    public int level;

    public void Clicked()
    {
        GameLevelManager.Instance.LoadLevel(level, gameMode);
        MenuManager.Instance.CloseAllMenus();
    }
}
