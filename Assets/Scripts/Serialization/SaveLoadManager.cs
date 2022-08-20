using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    public static int SAVE_LENGTH = 3;

    public static void SavePlayerData(int currentLevel, int newCoins, int currentChallengeLevel)
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.sav"))
        {
            int[] data = LoadPlayerData();
            int newCoinsValue = data[1] + newCoins;
            int newLevelValue = data[0];
            int newChallengeLevelValue = data[2];
            if(currentLevel > data[0])
            {
                newLevelValue = currentLevel;
            }
            if(currentChallengeLevel > data[2])
            {
                newChallengeLevelValue = currentChallengeLevel;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.sav", FileMode.Create);

            PlayerData playerData = new PlayerData(newLevelValue, newCoinsValue, newChallengeLevelValue);

            bf.Serialize(stream, playerData);
            stream.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.sav", FileMode.Create);

            PlayerData playerData = new PlayerData(currentLevel, newCoins, currentChallengeLevel);

            bf.Serialize(stream, playerData);
            stream.Close();
        }
    }

    public static int[] LoadPlayerData()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.sav", FileMode.Open);

            PlayerData playerData = (PlayerData)bf.Deserialize(stream);

            stream.Close();

            return playerData.data;
        }
        else
        {
            SavePlayerData(1, 0, 1);
            int[] currentData = new int[3];
            currentData[0] = 1;
            currentData[2] = 1;
            return currentData;
        }
    }

}

[Serializable]
public class PlayerData
{
    public int[] data;

    public PlayerData(int currentLevel, int coins, int currentChallengeLevel)
    {
        data = new int[3];
        data[0] = currentLevel;
        data[1] = coins;
        data[2] = currentChallengeLevel;
    }
}
