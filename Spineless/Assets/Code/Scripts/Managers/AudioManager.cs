using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

using UnityEngine.SceneManagement;
using Unity.VisualScripting;

/*
An audio manager that plays/ stops sound effects and music, and tracks global volumes.
*/

public class AudioManager : MonoBehaviour
{
    //list of sounds and music tracks added via inspector
    [SerializeField]
    private Sound[] sounds;
    [SerializeField]
    private Sound[] musicTracks;

    //local and global manager instances
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } } //to use any method from this manager call AudioManager.Instance."FunctionName"(); anywhere in any script
    public string CurrentTrack;
    public AudioSource CurrentTrackSource;
    public string CurrentSound;
    public AudioSource CurrentSoundSource;

    [SerializeField] private float musicFadeInDuration;
    [SerializeField] private float musicFadeOutDuration;

    //global volume values
    [SerializeField] private FloatReference globalSoundVolume;
    [SerializeField] private FloatReference globalMusicVolume;


    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            globalMusicVolume.Value = 0.5f;
            globalSoundVolume.Value = 0.8f;

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

            s.source.volume = globalSoundVolume.Value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        foreach (Sound s in musicTracks)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = globalMusicVolume.Value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start() // this will play sounds on start of the level, so main theme and what not
    {
        StopAllSounds();
        // else if (SceneManager.GetActiveScene().name == "GameBoard")
        // {
        //     Instance.PlayMusicTrack("Encounter Music");
        // } MUSIC PLAYED FROM PHONOGRAPH
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            //Instance.PlayMusicTrack("Encounter Music");
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Instance.PlayMusicTrack("Title Music");
        }
        if (SceneManager.GetActiveScene().name == "ShopRoom")
        {
            Instance.PlayMusicTrack("Shop Room Music");
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                          AUDIO MANAGER METHODS                             */
    /* -------------------------------------------------------------------------- */

    public void PlaySound(string name)  //Play a sound by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.PlaySound("Sound Name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        CurrentSound = s.name;
        CurrentSoundSource = s.source;
        s.source.PlayOneShot(s.source.clip, s.source.volume);
        MuffleMusic();
        Invoke("UnMuffleMusic", s.clip.length);
    }
    public void MuffleMusic()
    {
        //CurrentTrackSource.volume = CurrentTrackSource.volume * .4f;
    }
    public void UnMuffleMusic()
    {
        //CurrentTrackSource.volume = globalMusicVolume.Value;
    }
    public void StopSound(string name) //Stop a sound by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.StopSound("Sound Name");
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void PlayMusicTrack(string name)  //Play a music track by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.PlayTrack("Sound Name");
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
                //play music
                CurrentTrack = track.name;
                CurrentTrackSource = track.source;
                Debug.Log("Playing track: " + track.name);
                track.source.volume = 0;
                track.source.Play();
                StartCoroutine(FadeInMusic(track, musicFadeInDuration));
            }
            else
            {
                track.source.Stop();
            }
        }
    }
    public void StopMusicTrack(string name) //Stop a music track by passing its name assigned in inspector. Usage outside of this class: AudioManager.Instance.StopTrack("Sound Name");
    {
        Sound s = Array.Find(musicTracks, sound => sound.name == name);
        if (s == null)// if name of audio clip is wrong this will help debug
        {
            Debug.LogWarning("Music Track: " + name + " not found!");
            return;
        }
        StartCoroutine(FadeOutMusic(s, musicFadeOutDuration));
    }
    public void StopAllSounds()
    { //Stop all sounds. Usage outside of this class: AudioManager.Instance.StopAllSounds();
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
        foreach (Sound s in musicTracks)
        {
            s.source.Stop();
        }
    }

    private IEnumerator FadeInMusic(Sound track, float duration)
    {
        //fade in music
        float currentTime = 0;
        float startVolume = 0;
        while (currentTime < musicFadeInDuration)
        {
            currentTime += Time.deltaTime;
            track.source.volume = Mathf.Lerp(startVolume, globalMusicVolume.Value, currentTime / musicFadeInDuration);
            yield return null;
        }
        yield break;
    }
    private IEnumerator FadeOutMusic(Sound track, float duration)
    {
        //fade out music
        float currentTime = 0;
        float endVolume = 0;
        while (currentTime < musicFadeInDuration)
        {
            currentTime += Time.deltaTime;
            track.source.volume = Mathf.Lerp(globalMusicVolume.Value, endVolume, currentTime / musicFadeOutDuration);
            yield return null;
        }
        track.source.Stop();
        yield break;
    }
    /* -------------------------------------------------------------------------- */
    /*                           GETTERS AND SETTERS                              */
    /* -------------------------------------------------------------------------- */

    public float SoundVolume() //get global sound effects volume value
    {
        return globalSoundVolume.Value;
    }
    public float MusicVolume() //get global music volume value
    {
        return globalMusicVolume.Value;
    }
    public void SetSoundVolume(float newValue) //set new global sound effects volume value
    {
        globalSoundVolume.Value = newValue; //update global sound effects volume variable
        foreach (Sound s in sounds)
        {
            s.source.volume = newValue; //update volume of all sound effect
        }
    }
    public void SetMusicVolume(float newValue) //set new global music volume value
    {
        globalMusicVolume.Value = newValue; //update global music volume variable
        foreach (Sound track in musicTracks)
        {
            track.source.volume = newValue; //update volume of all music tracks
        }
    }
}