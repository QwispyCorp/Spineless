using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Eye of Foresight item allows player to select a card and reveal its value without executing its effect
public class Eye : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerEye);
        gameObject.SetActive(false);
    }
}
