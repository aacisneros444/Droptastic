using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Scroll Data")]
    private Vector3 panelLocation;
    public int numPanels;
    public int panelIndex;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public float panelWidth;

    [Header("Indicator")]
    public Image[] panelIndicators;
    public Color onPanelColor;

    private void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float difference = eventData.pressPosition.x - eventData.position.x;
        transform.position = panelLocation - new Vector3(difference, 0f, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && panelIndex < numPanels - 1)
            {
                newLocation += new Vector3(-Screen.width / 2, 0, 0);
                panelIndex++;
            }
            else if(percentage < 0 && panelIndex > 0)
            {
                panelIndex--;
                newLocation += new Vector3(Screen.width / 2, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
            for(int i = 0; i < panelIndicators.Length; i++)
            {
                if(i == panelIndex)
                {
                    panelIndicators[i].color = onPanelColor;
                }
                else
                {
                    panelIndicators[i].color = Color.white;
                }
            }
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
