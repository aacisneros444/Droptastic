using UnityEngine;

public class SetFramerate : MonoBehaviour
{
    void Update()
    {
        Application.targetFrameRate = 60;
    }
}
