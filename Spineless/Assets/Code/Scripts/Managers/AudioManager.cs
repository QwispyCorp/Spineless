using UnityEngine.Audio;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;
using Unity.VisualScripting;

/*
An audio manager that plays/ stops sound effects and music, and tracks global volumes.
*/

public class AudioManager : MonoBehaviour
{
    //list of sounds and music tracks added via inspector
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] musicTracks;

    //local and global manager instances
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } } //to use any method from this manager call AudioManager.Instance."FunctionName"(); anywhere in any script

    //global volume values
    private float globalSoundVolume;
    private float globalMusicVolume;


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

        foreach (Sound s in musicTracks)
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
    /*                          AUDIO MANAGER METHODS                             */
    /* -------------------------------------------------------------------------- */

    private void PlaySound(string name)  //Play a sound by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.PlaySound("Sound Name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    private void StopSound(string name) //Stop a sound by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.StopSound("Sound Name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    private void PlayMusicTrack(string name)  //Play a music track by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.PlayTrack("Sound Name");
    {
        Sound s = Array.Find(musicTracks, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Music Track: " + name + " not found!");
            return;
        }
        foreach (Sound track in musicTracks)
        {
            if (track.name == name)
            {
                track.source.Play();
            }
            else
            {
                track.source.Stop();
            }
        }
    }
    private void StopMusicTrack(string name) //Stop a music track by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.StopTrack("Sound Name");
    {
        Sound s = Array.Find(musicTracks, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Music Track: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    private void StopAllSounds()
    { //Stop all sounds. Usage outside of this class: AudioManager.Instance.StopAllSounds();
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    /* -------------------------------------------------------------------------- */
    /*                           GETTERS AND SETTERS                              */
    /* -------------------------------------------------------------------------- */

    private float SoundVolume() //get global sound effects volume value
    {
        return globalSoundVolume;
    }
    private float MusicVolume() //get global music volume value
    {
        return globalMusicVolume;
    }
    private void SetSoundVolume(float newValue) //set new global sound effects volume value
    {
        globalSoundVolume = newValue;
    }
    private void SetMusicVolume(float newValue) //set new global music volume value
    {
        globalMusicVolume = newValue;
    }
}