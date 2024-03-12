using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialVolumeUpdater : MonoBehaviour
{
    [SerializeField] private AudioSource[] spatialSounds;
    [SerializeField] private FloatReference globalMusicVolume;
    [SerializeField] private FloatReference globalSoundVolume;
    [SerializeField] private AudioSource spatialMusicTrack;
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
        if (spatialMusicTrack != null)
        {
            spatialMusicTrack.volume = globalMusicVolume.Value;
        }

        foreach (AudioSource source in spatialSounds)
        {
            source.volume = globalSoundVolume.Value;
        }
    }
    void UpdateMusicVolume()
    {
        if (spatialMusicTrack != null)
        {
            spatialMusicTrack.volume = globalMusicVolume.Value;
        }
    }
    void UpdateSoundVolume()
    {
        foreach (AudioSource source in spatialSounds)
        {
            source.volume = globalSoundVolume.Value;
        }
    }
}
