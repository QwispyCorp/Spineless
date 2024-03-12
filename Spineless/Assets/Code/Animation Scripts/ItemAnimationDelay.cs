using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script attached to animation objects to time when to destroy object and when to execute effect
public class ItemAnimationDelay : MonoBehaviour
{
    private AnimationClip[] itemAnimationClips;
    private float itemAnimationLength;

    void Start()
    {
        itemAnimationClips = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        itemAnimationLength = itemAnimationClips[0].length;

        StartCoroutine("ItemEffectDelay");
    }

    private IEnumerator ItemEffectDelay()
    {
        yield return new WaitForSeconds(itemAnimationLength);
        //execute item effect
        Destroy(gameObject);
    }
}
