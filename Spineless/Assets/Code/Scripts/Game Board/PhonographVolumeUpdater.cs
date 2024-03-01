using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonographVolumeUpdater : MonoBehaviour
{
    [SerializeField] private AudioSource[] phonographTracks;
    [SerializeField] private FloatReference globalMusicVolume;
    [SerializeField] private FloatReference globalSoundVolume;
    [SerializeField] private AudioSource phonographMusicTrack;
    void OnEnable()
    {
        MusicSlider.OnMusicVolumeChanged += UpdateMusicVolume;
        SoundsSlider.OnSoundVolumeChanged += UpdateSoundVolume;
    }
    void OnDisable()
    {
        MusicSlider.OnMusicVolumeChanged += UpdateMusicVolume;
        SoundsSlider.OnSoundVolumeChanged -= UpdateSoundVolume;
    }
    void Start()
    {
        if (phonographMusicTrack != null)
        {
            phonographMusicTrack.volume = globalMusicVolume.Value;
        }

        foreach (AudioSource source in phonographTracks)
        {
            source.volume = globalSoundVolume.Value;
        }
    }
    void UpdateMusicVolume()
    {
        if (phonographMusicTrack != null)
        {
            phonographMusicTrack.volume = globalMusicVolume.Value;
        }
    }
    void UpdateSoundVolume()
    {
        foreach (AudioSource source in phonographTracks)
        {
            source.volume = globalSoundVolume.Value;
        }
    }
}
