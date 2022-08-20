using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndSequence : MonoBehaviour
{
    [Header("References")]
    public GameObject gameEndBg;
    public CanvasGroup endMenuCanvasGroup;
    public TMP_Text endResultText;
    public TMP_Text coinsWonText;
    public ParticleSystem wonConfetti;
    public Button homeButton;
    public Button retryButton;
    public Button nextLevelButton;

    public void Animate(bool won)
    {
        homeButton.interactable = false;
        retryButton.interactable = false;
        nextLevelButton.interactable = false;

        LeanTween.alphaCanvas(endMenuCanvasGroup, 1f, 0.5f);
        LeanTween.alpha(gameEndBg, 1f, 0.5f);

        if (won)
        {
            coinsWonText.gameObject.SetActive(true);
            coinsWonText.text = "+" + GameLevelManager.Instance.levelData.coins.ToString();

            //Is there a next level?
            if(Resources.Load("Classic\\Level" + (GameLevelManager.Instance.levelData.level + 1)) != null)
            {
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                nextLevelButton.gameObject.SetActive(false);
            }

            int textResult = Random.Range(7, 8);

            switch(textResult)
            {
                case 0:
                    endResultText.text = "Nice!";
                    break;
                case 1:
                    endResultText.text = "Awesome!";
                    break;
                case 2:
                    endResultText.text = "Baller!";
                    break;
                case 3:
                    endResultText.text = "Buckets!";
                    break;
                case 4:
                    endResultText.text = "Amazing!";
                    break;
                case 5:
                    endResultText.text = "Golazo!";
                    break;
                case 6:
                    endResultText.text = "Touchdown!";
                    break;
                case 7:
                    endResultText.text = "Droptastic!";
                    break;
                default:
                    endResultText.text = "NICE!";
                    break;
            }

            LeanTween.delayedCall(0.35f, () => {
                wonConfetti.Play();
                //AudioManager.Instance.playAudioClip(5);
            });
        }
        else
        {
            coinsWonText.gameObject.SetActive(false);
            nextLevelButton.gameObject.SetActive(false);

            endResultText.text = "Game Over!";
        }


        LeanTween.delayedCall(0.55f, () => {
            MakeButtonsInetractable();
        });

        AudioManager.Instance.comboIndex = 0;
    }

    public void ResetAnimationValues()
    {
        endMenuCanvasGroup.alpha = 0f;
        gameEndBg.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
    }

    public void MakeButtonsInetractable()
    {
        homeButton.interactable = true;
        retryButton.interactable = true;
        nextLevelButton.interactable = true;
    }
}
