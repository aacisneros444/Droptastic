using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    [Header("Current Level Data")]
    [HideInInspector] public LevelDataN levelData;

    [Header("Current Level State")]
    [HideInInspector] public bool levelLoaded;
    [HideInInspector] public float timeLeft;
    [HideInInspector] public int ballsMissed;
    [HideInInspector] public int ballsScored;
    [HideInInspector] bool showVideoAdOnCondition;
    [HideInInspector] public bool clickedRewardedVideoAdButton;
    [HideInInspector] public int lastRoundedTimeValue;

    [Header("UI References")]
    public TMP_Text ballsScoredText;
    public TMP_Text timeLeftText;
    public TMP_Text ballsMissedText;
    public Image radialTimeLeft;

    [Header("GameObject References")]
    public GameObject levelParentObj;
    public GameObject rewardedVideoButton_MoreTime;

    [Header("Script References")]
    public AnimateTextColor ballsScoredAnimateText;
    public AnimateTextColor ballsMissedAnimateText;
    public RewardedText rewardedText;
    public DropBall dropBall;
    public Button_RewardedVideo button_RewardedVideo;

    private void Awake()
    {
        Instance = this;
    }

    //Destroys old level prefab and loads level data into this class.
    public void LoadLevel(int level, int gamemode)
    {
        if (levelData != null)
        {
            Destroy(levelData.gameObject);
        }

        GameObject levelPrefab = null;
        if(gamemode == 0)
        {
            levelPrefab = (GameObject)Resources.Load("Classic\\Level" + level);
        }
        else if(gamemode == 1)
        {
            levelPrefab = (GameObject)Resources.Load("Challenge\\Level" + level);
        }
        GameObject levelPrefabGameObj = Instantiate(levelPrefab, Vector3.zero,
            Quaternion.identity, levelParentObj.transform);
        levelData = levelPrefabGameObj.GetComponent<LevelDataN>();

        ResetLevelState();

        levelParentObj.SetActive(true);
        levelLoaded = true;
    }

    //Initialized already loaded level data.
    public void LoadCurrentLevel()
    {
        ResetLevelState();

        levelParentObj.SetActive(true);
        levelLoaded = true;
    }

    //Resets global level variables and UI.
    public void ResetLevelState()
    {
        timeLeft = levelData.levelTime;
        ballsMissed = 0;
        ballsScored = 0;

        ballsScoredText.text = (levelData.ballsNeededToWin - ballsScored) + " to go!";
        ballsMissedText.text = ballsMissed.ToString() + " / " + levelData.maxBallMisses + " missed";

        for(int i = 0; i < levelData.rotators.Count; i++)
        {
            levelData.rotators[i].GetComponent<Rotator>().ResetRotator();
        }

        rewardedVideoButton_MoreTime.SetActive(false);
        clickedRewardedVideoAdButton = false;
        DecideIfLevelWillShowRewardedVideoAdButton();

        AdManagerG.Instance.IncrementGamesSinceLastAd();

        lastRoundedTimeValue = -1;
        StartCoroutine(WaitToAcceptInput());

        DestroyOldBalls();
    }

    //Destroy any balls left over from previous rounds
    public void DestroyOldBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        for(int i = 0; i < balls.Length; i++)
        {
            if(balls[i].gameObject.layer == 8)
            {
                Destroy(balls[i]);
            }
        }
    }

    private void Update()
    {
        if (levelLoaded)
        {
            DecreaseTime();
            CheckForEndLevelConditions();
            CheckShouldDisplayRewardedVideoAdButton();
        }
    }

    //Checks if player met level requirements (won or lost).
    private void CheckForEndLevelConditions()
    {
        if(ballsScored >= levelData.ballsNeededToWin)
        {
            levelLoaded = false;
            dropBall.acceptingInput = false;
            MenuManager.Instance.OpenMenu(2, 1);

            AudioManager.Instance.PlayAudioClip(7);

            if(levelData.gameMode == 0)
            {
                SaveLoadManager.SavePlayerData(levelData.level + 1, levelData.coins, -1);
            }
            else if (levelData.gameMode == 1)
            {
                SaveLoadManager.SavePlayerData(-1, levelData.coins, levelData.level + 1);
            }
            StartCoroutine(DelayEndInterstitalAd());
        }

        if(ballsMissed >= levelData.maxBallMisses)
        {
            levelLoaded = false;
            dropBall.acceptingInput = false;
            MenuManager.Instance.OpenMenu(2, 0);

            AudioManager.Instance.PlayAudioClip(8);

            StartCoroutine(DelayEndInterstitalAd());
        }

        if(timeLeft <= 0)
        {
            levelLoaded = false;
            dropBall.acceptingInput = false;

            AudioManager.Instance.PlayAudioClip(8);

            MenuManager.Instance.OpenMenu(2, 0);
            StartCoroutine(DelayEndInterstitalAd());
        }
    }

    //Calculates if a condition is true that will prompt the rewarded video ad button.
    public void CheckShouldDisplayRewardedVideoAdButton()
    {
        if (!clickedRewardedVideoAdButton)
        {
            if (timeLeft <= levelData.levelTime / 3)
            {
                if (showVideoAdOnCondition)
                {
                    if (!rewardedVideoButton_MoreTime.activeSelf)
                    {
                        rewardedVideoButton_MoreTime.SetActive(true);
                    }
                }
            }
        }
    }

    //Calculates a random number to determine if a rewarded video ad option will be available in the level.
    public void DecideIfLevelWillShowRewardedVideoAdButton()
    {
        if (AdManagerG.Instance.rewardBasedVideo.IsLoaded())
        {
            int showRewardedVideoAdOnConditionInt = Random.Range(0, 5);
            if (showRewardedVideoAdOnConditionInt < 2)
            {
                showVideoAdOnCondition = true;
            }
            else
            {
                showVideoAdOnCondition = false;
            }
        }
    }

    //Updates countdown timer and changes displayed text.
    private void DecreaseTime()
    {
        timeLeft -= Time.deltaTime;
        timeLeftText.text = Mathf.Ceil(timeLeft).ToString();
        radialTimeLeft.fillAmount = timeLeft / levelData.levelTime;

        if (timeLeft <= 3)
        {
            int roundedTime = (int)Mathf.Ceil(timeLeft);

            if (lastRoundedTimeValue == -1)
            {
                lastRoundedTimeValue = roundedTime;
                AudioManager.Instance.PlayAudioClip(6);
                if (rewardedVideoButton_MoreTime.activeSelf)
                {
                    button_RewardedVideo.Swell();
                }
            }
            if((int)Mathf.Ceil(timeLeft) < lastRoundedTimeValue)
            {
                lastRoundedTimeValue = roundedTime;
                AudioManager.Instance.PlayAudioClip(6);

                if (rewardedVideoButton_MoreTime.activeSelf)
                {
                    button_RewardedVideo.Swell();
                }
            }
        }
    }

    //Increments balls missed, changes displayed text, starts a camera shake, starts a text color animation.
    public void BallMissed()
    {
        if (!levelLoaded)
            return;

        ballsMissed++;
        if(ballsMissed < 0)
        {
            ballsMissed = 0;
        }

        ballsMissedText.text = ballsMissed.ToString() + " / " + levelData.maxBallMisses + " missed";
        ballsMissedAnimateText.Animate();

        CameraShake.Instance.ShakeCamera();

        AudioManager.Instance.PlayAudioClip(4);
    }

    //Increments balls scored, changes displayed text, and plays a particle effect, starts a text color animation..
    public void ScoredBallInCup()
    {
        if (!levelLoaded)
            return;

        ballsScored++;
        if (ballsScored < 0)
        {
            ballsScored = 0;
        }

        ballsScoredText.text = (levelData.ballsNeededToWin - ballsScored) + " to go!"; ;
        ballsScoredAnimateText.Animate();
    }

    //Watched a rewarded video -> Give rewards.
    public void RewardedVideoComplete(int rewardType)
    {
        if(rewardType == 0)
        {
            float timeAdded = 0;
            if(timeLeft + 15 > levelData.levelTime)
            {
                timeAdded = levelData.levelTime - timeLeft;
                timeLeft = levelData.levelTime;
            }
            else
            {
                timeAdded = 15;
                timeLeft = timeLeft + 15;
            }

            rewardedText.Animate("+" + (int)timeAdded + "\r\n seconds");
        }

        if(rewardType == 1)
        {
            ballsMissed = 0;
        }

        AudioManager.Instance.PlayAudioClip(5);
    }

    //Used to delay taking input while level is loading.
    IEnumerator WaitToAcceptInput()
    {
        yield return new WaitForSeconds(0.1f);
        dropBall.acceptingInput = true;
    }

    //Used to delay the playing of an intersitial ad.
    IEnumerator DelayEndInterstitalAd()
    {
        yield return new WaitForSeconds(0.7f);
        AdManagerG.Instance.CheckShowAd();
    }

}
