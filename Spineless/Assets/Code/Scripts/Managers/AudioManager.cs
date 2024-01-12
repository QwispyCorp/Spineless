using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;

//This is an Audio Manager done by Brakeys on YouTube

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() // this will play sounds on start of the level, so main theme and what not
    {
        //Play("AudioName");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

}
// Below is the code you put to reference the audio clip anywhere in the script. 
// FindObjectOfType<AudioManager>().Play("AudioName");