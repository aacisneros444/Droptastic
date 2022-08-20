using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSaveOnStart : MonoBehaviour
{
    private void Start()
    {
        int[] saveData = SaveLoadManager.LoadPlayerData();
        if (saveData.Length < SaveLoadManager.SAVE_LENGTH)
        {
            SaveLoadManager.SavePlayerData(saveData[0], saveData[1], 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int[] saveData = SaveLoadManager.LoadPlayerData();
            Debug.Log("Current classic level: " + saveData[0]);
            Debug.Log("Coins: " + saveData[1]);
            Debug.Log("Current challenge level: " + saveData[2]);
        }
    }
}
