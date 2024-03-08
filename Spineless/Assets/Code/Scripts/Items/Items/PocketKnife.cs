using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pocket knife item allows player to select one enemy card to reveal without executing its effect
public class PocketKnife : MonoBehaviour, Interactable
{
    public void Interact()
    {
        gameObject.SetActive(false); //hide/ destroy item after effect is used
    }
}
