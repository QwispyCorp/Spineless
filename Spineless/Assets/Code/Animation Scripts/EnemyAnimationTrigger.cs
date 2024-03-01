using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    // get lengths of animation clips for finger chops, to calculate delay times for coroutine in switching states
    private float lengthChop1;
    private float lengthChop2;
    private float lengthChop3;
    private float lengthChop4;
    private float lengthChop5;
    public static float CurrentEnemyChopAnimLength;
    public delegate void EnemyAnimationFinished();
    public static event EnemyAnimationFinished OnEnemyAnimationFinished;
    [SerializeField] private Animator enemyAnimator;
    void OnEnable()
    {
        EnemyHealthTest.OnEnemyFingerLost += PlayChopAnimation;
    }
    void OnDisable()
    {
        EnemyHealthTest.OnEnemyFingerLost -= PlayChopAnimation;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateAnimClipTimes();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayChopAnimation()
    {
        switch (EnemyHealthTest.Instance.GetCurrentHealth())
        {
            case 1:
                CurrentEnemyChopAnimLength = lengthChop1;
                enemyAnimator.SetTrigger("Chop 1");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 2:
                CurrentEnemyChopAnimLength = lengthChop2;
                enemyAnimator.SetTrigger("Chop 2");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 3:
                CurrentEnemyChopAnimLength = lengthChop3;
                enemyAnimator.SetTrigger("Chop 3");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 4:
                CurrentEnemyChopAnimLength = lengthChop4;
                enemyAnimator.SetTrigger("Chop 4");
                StartCoroutine("AnimationTurnDelay");
                break;
            case 5:
                CurrentEnemyChopAnimLength = lengthChop5;
                enemyAnimator.SetTrigger("Chop 5");
                StartCoroutine("AnimationTurnDelay");
                break;
            default:
                break;
        }
    }
    private IEnumerator AnimationTurnDelay()
    {
        yield return new WaitForSeconds(CurrentEnemyChopAnimLength);

        if (OnEnemyAnimationFinished != null)
        {
            Debug.Log("On Enemy Animation Finished");
            OnEnemyAnimationFinished?.Invoke();
        }
    }
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = enemyAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Monster Chop 5":
                    lengthChop5 = clip.length;
                    Debug.Log("Length Clip 5: " + clip.length);
                    break;
                case "Monster Chop 4":
                    lengthChop4 = clip.length;
                    Debug.Log("Length Clip 4: " + clip.length);
                    break;
                case "Monster Chop 3":
                    lengthChop3 = clip.length;
                    Debug.Log("Length Clip 3: " + clip.length);
                    break;
                case "Monster Chop 2":
                    lengthChop2 = clip.length;
                    Debug.Log("Length Clip 2: " + clip.length);
                    break;
                case "Monster Chop 1":
                    lengthChop1 = clip.length;
                    Debug.Log("Length Clip 1 " + clip.length);
                    break;
            }
        }
    }
}
