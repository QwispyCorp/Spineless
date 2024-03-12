using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [SerializeField] private AudioSource enemyChopSound1;
    [SerializeField] private AudioSource enemyChopSound2;
    [SerializeField] private AudioSource enemyChopSound3;
    [SerializeField] private AudioSource enemyIdleSound;
    [SerializeField] private IntegerReference enemyHealth;
    void Start()
    {
        enemyIdleSound.Play();
    }

    void OnEnable()
    {
        EnemyHealthTest.OnEnemyFingerLost += PlayChopSound;

        EnemyAnimationTrigger.OnEnemyAnimationFinished += PlayIdleSound;
    }
    void OnDisable()
    {
        EnemyHealthTest.OnEnemyFingerLost -= PlayChopSound;
        EnemyAnimationTrigger.OnEnemyAnimationFinished -= PlayIdleSound;
    }

    void PlayIdleSound()
    {
        enemyIdleSound.Play();
    }
    void StopIdleSound()
    {
        enemyIdleSound.Stop();
    }

    void PlayChopSound()
    {
        StopIdleSound();
        switch (enemyHealth.Value)
        {
            case 1:
                enemyChopSound3.Play();
                break;
            case 2:
                enemyChopSound2.Play();
                break;
            case 3:
                enemyChopSound2.Play();
                break;
            case 4:
                enemyChopSound1.Play();
                break;
            case 5:
                enemyChopSound1.Play();
                break;
            default:
                break;
        }
    }

    void StopChopSound()
    {

    }
}
