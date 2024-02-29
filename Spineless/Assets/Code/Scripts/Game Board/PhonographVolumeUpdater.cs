using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonographVolumeUpdater : MonoBehaviour
{
    [SerializeField] private FloatReference globalMusicVolume;
    [SerializeField] private AudioSource phonographTrack;
    void OnEnable()
    {
        MusicSlider.OnMusicVolumeChanged += UpdateVolume;
    }
    void OnDisable()
    {
        MusicSlider.OnMusicVolumeChanged += UpdateVolume;
    }
    void Start()
    {
        phonographTrack.volume = globalMusicVolume.Value;
    }
    void UpdateVolume()
    {
        phonographTrack.volume = globalMusicVolume.Value;
    }
}
