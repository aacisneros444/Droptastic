using UnityEngine;

public class Button_Home : MonoBehaviour, IButton
{
    public void Clicked()
    {
        int currentGameMode = GameLevelManager.Instance.levelData.gameMode;
        if(currentGameMode == 0)
        {
            MenuManager.Instance.OpenMenu(1);
        }
        else if(currentGameMode == 1)
        {
            MenuManager.Instance.OpenMenu(4);
        }
    }
}
