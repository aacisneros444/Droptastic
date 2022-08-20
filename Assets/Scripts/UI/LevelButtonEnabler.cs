using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonEnabler : MonoBehaviour
{
    public Color levelCompletionColor;
    public GameObject[] buttonObjs;
    private Image[] images;
    private Button[] buttons;
    private int[] levels;
    public int levelSaveIndex;

    int maxLevelInGroup;

    private void Awake()
    {
        images = new Image[buttonObjs.Length];
        buttons = new Button[buttonObjs.Length];
        levels = new int[buttonObjs.Length];

        for (int i = 0; i < buttonObjs.Length; i++)
        {
            images[i] = buttonObjs[i].GetComponent<Image>();
            buttons[i] = buttonObjs[i].GetComponent<Button>();
            levels[i] = buttonObjs[i].GetComponent<Button_LoadLevel>().level;
        }
    }

    private void OnEnable()
    {
        int[] data = SaveLoadManager.LoadPlayerData();

        for(int i = 0; i < buttonObjs.Length; i++)
        {
            bool isInteractable = false;

            if(levels[i] < data[levelSaveIndex])
            {
                images[i].color = levelCompletionColor;
                buttons[i].interactable = true;
                isInteractable = true;
            }

            if(levels[i] == data[levelSaveIndex])
            {
                images[i].color = Color.white;
                buttons[i].interactable = true;
                isInteractable = true;
            }

            if (!isInteractable)
            {
                buttons[i].interactable = false;
            }
        }
    }
}
