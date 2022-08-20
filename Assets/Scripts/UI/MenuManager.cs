using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("References")]
    public GameObject levelObjects;
    public GameObject mainMenuObj;
    public GameObject levelSelectMenuObj;
    public GameObject challengeLevelSelectMenuObj;
    public GameObject gameEndMenuObj;
    public GameObject shopMenu;
    public GameObject miscMenu;
    public GameEndSequence gameEndSequence;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(int menuIndex)
    {
        //Open main menu
        if(menuIndex == 0)
        {
            mainMenuObj.SetActive(true);
            levelSelectMenuObj.SetActive(false);
            challengeLevelSelectMenuObj.SetActive(false);
            shopMenu.SetActive(false);
            if (!miscMenu.activeSelf)
            {
                miscMenu.SetActive(true);
            }
        }

        //Open level select menu
        if (menuIndex == 1)
        {
            mainMenuObj.SetActive(false);
            miscMenu.SetActive(false);
            levelSelectMenuObj.SetActive(true);

            //Just finished a game, clean things up.
            if (gameEndMenuObj.activeSelf)
            {
                gameEndSequence.ResetAnimationValues();
                gameEndMenuObj.SetActive(false);
                levelObjects.SetActive(false);
            }
        }

        //Open shop menu
        if (menuIndex == 3)
        {
            mainMenuObj.SetActive(false);
            shopMenu.SetActive(true);
        }

        //Open challenge level select menu
        if (menuIndex == 4)
        {
            mainMenuObj.SetActive(false);
            miscMenu.SetActive(false);
            challengeLevelSelectMenuObj.SetActive(true);

            //Just finished a game, clean things up.
            if (gameEndMenuObj.activeSelf)
            {
                gameEndSequence.ResetAnimationValues();
                gameEndMenuObj.SetActive(false);
                levelObjects.SetActive(false);
            }
        }
    }

    public void OpenMenu(int menuIndex, int secondaryState)
    {
        //Open game end menu
        if (menuIndex == 2)
        {
            if (secondaryState == 0)
            {
                gameEndMenuObj.SetActive(true);
                gameEndSequence.Animate(false);
            }

            if(secondaryState == 1)
            {
                gameEndMenuObj.SetActive(true);
                gameEndSequence.Animate(true);
            }
        }
    }

    public void CloseAllMenus()
    {
        mainMenuObj.SetActive(false);
        levelSelectMenuObj.SetActive(false);
        challengeLevelSelectMenuObj.SetActive(false);
        if (gameEndMenuObj.activeSelf)
        {
            gameEndMenuObj.SetActive(false);
            gameEndSequence.ResetAnimationValues();
        }
    }
}
