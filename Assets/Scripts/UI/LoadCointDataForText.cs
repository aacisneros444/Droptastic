using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCointDataForText : MonoBehaviour
{
    public TMP_Text coinText;

    private void OnEnable()
    {
        int[] data = SaveLoadManager.LoadPlayerData();
        coinText.text = data[1].ToString("N0");
    }
}
