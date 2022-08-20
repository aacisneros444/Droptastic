using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBall : MonoBehaviour
{
    public bool acceptingInput;
    public List<GameObject> ballsQueue = new List<GameObject>();
    public Transform dropTransform;
    public GameObject ballPrefab;
    public Transform ballParent;

    private void Update()
    {
        if (acceptingInput)
        {
            if (GameLevelManager.Instance.levelLoaded)
            {
                if (Input.GetMouseButtonUp(0))
                {          
                    GameObject ball = Instantiate(ballPrefab, dropTransform.position, Quaternion.identity);
                    ball.transform.parent = ballParent;
                    ballsQueue.Add(ball);

                    ballsQueue[0].layer = 8;

                    ballsQueue.RemoveAt(0);
                }
            }
        }
    }
}
