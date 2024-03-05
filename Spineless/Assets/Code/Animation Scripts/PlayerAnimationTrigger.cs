using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    // get lengths of animation clips for finger chops, to calculate delay times for coroutine in switching states
    private float lengthChop1;
    private float lengthChop2;
    private float lengthChop3;
    private float lengthChop4;
    private float lengthChop5;
    public static float CurrentChopAnimLength;
    public delegate void AnimationFinished();
    public static event AnimationFinished OnAnimationFinished;
    [SerializeField] private Animator playerAnimator;

    void OnEnable()
    {
        PlayerHealthTest.OnPlayerFingerGained += GainFinger;
        PlayerHealthTest.OnPlayerFingerLost += PlayChopAnimation;
    }
    void OnDisable()
    {
        PlayerHealthTest.OnPlayerFingerGained -= GainFinger;
        PlayerHealthTest.OnPlayerFingerLost -= PlayChopAnimation;
    }

    void Start()
    {
        UpdateAnimClipTimes();
    }

    void PlayChopAnimation()
    {
        switch (PlayerHealthTest.Instance.GetCurrentHealth())
        {
            case 1:
                CurrentChopAnimLength = lengthChop1;
                playerAnimator.SetTrigger("Chop 1");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 2:
                CurrentChopAnimLength = lengthChop2;
                playerAnimator.SetTrigger("Chop 2");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 3:
                CurrentChopAnimLength = lengthChop3;
                playerAnimator.SetTrigger("Chop 3");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 4:
                CurrentChopAnimLength = lengthChop4;
                playerAnimator.SetTrigger("Chop 4");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 5:
                CurrentChopAnimLength = lengthChop5;
                playerAnimator.SetTrigger("Chop 5");
                StartCoroutine("AnimationTurnDelay");
                break;
            default:
                break;
        }
    }

    void GainFinger() //Stapler animations with time delay will go here?
    {
        switch (PlayerHealthTest.Instance.GetCurrentHealth())
        {
            case 1:
                playerAnimator.SetTrigger("Idle 2");
                break;
            case 2:
                playerAnimator.SetTrigger("Idle 3");
                break;
            case 3:
                playerAnimator.SetTrigger("Idle 4");
                break;
            case 4:
                playerAnimator.SetTrigger("Idle 5");
                break;
            default:
                break;
        }
    }

    private IEnumerator AnimationTurnDelay()
    {
        yield return new WaitForSeconds(CurrentChopAnimLength);
        if (PlayerHealthTest.Instance.GetCurrentHealth() == 0)
        {
            //play lose animation? or whatever

            //wait time for lose animation to end?
            //then let player health manager know losing animations are finished
            if (OnAnimationFinished != null)
            {
                OnAnimationFinished?.Invoke();
            }
        }
        else
        {
            if (OnAnimationFinished != null)
            {
                OnAnimationFinished?.Invoke();
            }
        }
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = playerAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player Chop 5":
                    lengthChop5 = clip.length;
                    break;
                case "Player Chop 4":
                    lengthChop4 = clip.length;
                    break;
                case "Player Chop 3":
                    lengthChop3 = clip.length;
                    break;
                case "Player Chop 2":
                    lengthChop2 = clip.length;
                    break;
                case "Player Chop 1":
                    lengthChop1 = clip.length;
                    break;
            }
        }
    }

}
