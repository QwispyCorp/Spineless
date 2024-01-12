using UnityEngine.Audio;
using UnityEngine;
using System;

//This is an array to use in conjunction with the Audio Manager done by Brackeys on YouTube

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}