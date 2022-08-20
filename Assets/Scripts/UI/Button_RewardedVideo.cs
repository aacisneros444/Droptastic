using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_RewardedVideo : MonoBehaviour, IButton
{
    public int rewardType;
    private int swellScalar = 1;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Clicked()
    {
        GameLevelManager.Instance.clickedRewardedVideoAdButton = true;
        AdManagerG.Instance.ShowRewardedVideoAd();
        gameObject.SetActive(false);
    }

    public void Swell()
    {
        LeanTween.scale(rectTransform, new Vector3(0.0065f, 0.0065f, 0.0065f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.delayedCall(0.5f, () => {
            DeSwell();
        });
    }

    public void DeSwell()
    {
        LeanTween.scale(gameObject, new Vector3(0.005f, 0.005f, 0.005f), 0.5f).setEase(LeanTweenType.easeInBounce);
    }
}
