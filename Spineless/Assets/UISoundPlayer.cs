using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        AudioManager.Instance.PlaySound(soundName);
    }
}
