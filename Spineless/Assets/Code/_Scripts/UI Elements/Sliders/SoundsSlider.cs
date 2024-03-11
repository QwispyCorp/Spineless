using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundsSlider : MonoBehaviour
{
    private Slider soundsSlider;
    [SerializeField]
    private FloatReference globalSoundsVolume;
    public delegate void SoundVolumeChanged();
    public static event SoundVolumeChanged OnSoundVolumeChanged;
    void Awake()
    {
        soundsSlider = GetComponent<Slider>();
        soundsSlider.value = globalSoundsVolume.Value; //make sure the slider value is consistent with what the global volume is
    }
    void Start()
    {
        soundsSlider.onValueChanged.AddListener(val => AudioManager.Instance.SetSoundVolume(val));  //when slider value changes, update volume in audio manager
        soundsSlider.onValueChanged.AddListener(val => UpdateGlobalVolume());
    }

    void UpdateGlobalVolume()
    {
        if (OnSoundVolumeChanged != null)
        {
            OnSoundVolumeChanged?.Invoke(); //SEND MESSAGE THAT MUSIC VOLUME CHANGED
        }
    }
}
