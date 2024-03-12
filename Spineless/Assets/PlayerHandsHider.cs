using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandsHider : MonoBehaviour
{
    [SerializeField] private GameObject[] RightHandObjects;

    void OnEnable()
    {
        ItemMouseInteraction.OnItemUsed += HideHand;
        ItemAnimationDelay.OnItemAnimationEnded += ShowHand;
    }
    void OnDisable()
    {
        ItemMouseInteraction.OnItemUsed -= HideHand;
        ItemAnimationDelay.OnItemAnimationEnded -= ShowHand;
    }


    private void HideHand()
    {
        foreach (GameObject gameObject in RightHandObjects)
        {
            gameObject.SetActive(false);
        }
    }
    private void ShowHand()
    {
        foreach (GameObject gameObject in RightHandObjects)
        {
            gameObject.SetActive(true);
        }
    }
}
