using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        {
            Destroy(col.gameObject);

            GameLevelManager.Instance.BallMissed();
        }
    }
}
