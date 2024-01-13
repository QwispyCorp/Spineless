using UnityEngine.Audio;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    //list of sounds
    [SerializeField] private Sound[] sounds;

    //local and global manager instances
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } } //to use any public method call AudioManager.Instance."FunctionName"(); anywhere in any script

    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start() // this will play sounds on start of the level, so main theme and what not
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            //AudioManager.Instance.Play("Title Music");  uncomment this when title music is added
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                       AudioManager Methods                                 */
    /* -------------------------------------------------------------------------- */
    
    private void Play(string name)  //Play a sound using its name as input. Usage: AudioManager.Instance.Play("name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    private void Stop(string name) //Stop a sound using its name as input. Usage: AudioManager.Instance.Stop("name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}