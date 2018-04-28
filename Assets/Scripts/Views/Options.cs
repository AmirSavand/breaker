using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sliderVolumeMaster;
    public Slider sliderVolumeMusic;
    public Slider sliderVolumeEffects;
    public Slider sliderVolumeMenu;

    public Toggle toggleEnableVibrate;

    void OnEnable ()
    {
        float volumeMaster, volumeMusic, volumeEffects, volumeMenu;
        
        audioMixer.GetFloat ("masterVol", out volumeMaster);
        audioMixer.GetFloat ("musicVol", out volumeMusic);
        audioMixer.GetFloat ("effectsVol", out volumeEffects);
        audioMixer.GetFloat ("menuVol", out volumeMenu);

        sliderVolumeMaster.value = volumeMaster;
        sliderVolumeMusic.value = volumeMusic;
        sliderVolumeEffects.value = volumeEffects;
        sliderVolumeMenu.value = volumeMenu;

        toggleEnableVibrate.isOn = Storage.EnableVibrate;
    }

    public void onOptionChange ()
    {
        Storage.Save ();
    }

    public void setVolumeMaster (float value)
    {
        Storage.VolumeMaster = (int)value;
        audioMixer.SetFloat ("masterVol", value);
    }

    public void setVolumeMusic (float value)
    {
        Storage.VolumeMusic = (int)value;
        audioMixer.SetFloat ("musicVol", value);
    }

    public void setVolumeEffects (float value)
    {
        Storage.VolumEffects = (int)value;
        audioMixer.SetFloat ("effectsVol", value);
    }

    public void setVolumeMenu (float value)
    {
        Storage.VolumeMenu = (int)value;
        audioMixer.SetFloat ("menuVol", value);
    }

    public void setEnableVibrate (bool toggle)
    {
        Storage.EnableVibrate = toggle;
    }
}
