using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDataN : MonoBehaviour
{
    [Header("Level Parameters")]
    public int gameMode;
    public int level;
    public float levelTime;
    public int maxBallMisses;
    public int ballsNeededToWin;
    public int coins;
    public List<GameObject> rotators;
}
