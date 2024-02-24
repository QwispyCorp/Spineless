using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider musicSlider;
    [SerializeField]
    private FloatReference globalMusicVolume;
    public delegate void MusicVolumeChanged();
    public static event MusicVolumeChanged OnMusicVolumeChanged;
    void Awake()
    {
        musicSlider = GetComponent<Slider>();
        musicSlider.value = globalMusicVolume.Value; //make sure the slider value is consistent with what the global volume is
    }
    void Start()
    {
        musicSlider.onValueChanged.AddListener(val => AudioManager.Instance.SetMusicVolume(val)); //when slider value changes, update volume in audio manager
        musicSlider.onValueChanged.AddListener(val => UpdateGlobalVolume(val));
    }

    void UpdateGlobalVolume(float newVolume)
    {
        if (OnMusicVolumeChanged != null)
        {
            Debug.Log("Slider debug point");
            OnMusicVolumeChanged?.Invoke(); //SEND MESSAGE THAT MUSIC VOLUME CHANGED
        }
    }
}
