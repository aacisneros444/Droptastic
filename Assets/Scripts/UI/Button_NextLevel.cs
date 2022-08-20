using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_NextLevel : MonoBehaviour, IButton
{
    public void Clicked()
    {
        int nextLevel = GameLevelManager.Instance.levelData.level + 1;
        GameLevelManager.Instance.LoadLevel(nextLevel, GameLevelManager.Instance.levelData.gameMode);
        MenuManager.Instance.CloseAllMenus();
    }
}
