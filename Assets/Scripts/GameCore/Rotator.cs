using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;
    public float currentSpeed;
    private Quaternion inititalRot;

    private void Start()
    {
        currentSpeed = speed;
        inititalRot = transform.rotation;
    }

    private void Update()
    {
        if (GameLevelManager.Instance.levelLoaded)
        {
            transform.Rotate(transform.up, currentSpeed * Time.deltaTime);
        }
    }

    public void ResetRotator()
    {
        currentSpeed = speed;
        transform.rotation = inititalRot;
    }
}
