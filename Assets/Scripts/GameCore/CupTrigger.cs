using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    // 0 = default cup, 1 = fast forward cup
    public int cupType = 0;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ball")
        {
            Destroy(col.gameObject);

            if(cupType == 0)
            {
                GameLevelManager.Instance.ScoredBallInCup();
                PlayCollisionsEffects(0, 0, col.transform.position);
            }
            else if(cupType == 1)
            {
                GameLevelManager.Instance.ScoredBallInCup();
                transform.parent.transform.parent.GetComponent<Rotator>().currentSpeed += 20;
                PlayCollisionsEffects(9, 0, col.transform.position);
                ObjectPooler.Instance.SpawnFromPoolOriginalTransform(1).GetComponent<ParticleSystem>().Play();
            }
            else if(cupType == 2)
            {
                GameLevelManager.Instance.ScoredBallInCup();
                transform.parent.transform.parent.GetComponent<Rotator>().currentSpeed *= -1f;
                PlayCollisionsEffects(10, 0, col.transform.position);
                ObjectPooler.Instance.SpawnFromPoolOriginalTransform(1).GetComponent<ParticleSystem>().Play();
            }
        }
    }

    public void PlayCollisionsEffects(int audioIndex, int particleIndex, Vector3 pos)
    {
        AudioManager.Instance.PlayAudioClip(audioIndex);

        GameObject particleSystemObj = ObjectPooler.Instance.SpawnFromPoolOriginalRotation(0,  pos);
        particleSystemObj.GetComponent<ParticleSystem>().Play();
    }

}
