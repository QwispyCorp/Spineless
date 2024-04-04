using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script attached to animation objects to time when to destroy object and when to execute effect
public class ItemAnimationDelay : MonoBehaviour
{
    [SerializeField] private Item itemSO;
    private AnimationClip[] itemAnimationClips;
    private float itemAnimationLength;
    public delegate void ItemAnimationEnded();
    public static event ItemAnimationEnded OnItemAnimationEnded;

    void Start()
    {
        itemAnimationClips = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        itemAnimationLength = itemAnimationClips[0].length;

        StartCoroutine("ItemEffectDelay");
    }

    private IEnumerator ItemEffectDelay()
    {
        yield return new WaitForSeconds(itemAnimationLength);

        if (OnItemAnimationEnded != null)
        {
            OnItemAnimationEnded?.Invoke();
        }
        //execute item effect
        //Destroy(gameObject);
    }
}
