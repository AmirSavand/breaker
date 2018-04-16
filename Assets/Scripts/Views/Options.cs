using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider effectsVolSlider;
    public Slider menuVolSlider;

    void OnEnable ()
    {
        float masterVol, musicVol, effectsVol, menuVol;
        
        audioMixer.GetFloat ("masterVol", out masterVol);
        audioMixer.GetFloat ("musicVol", out musicVol);
        audioMixer.GetFloat ("effectsVol", out effectsVol);
        audioMixer.GetFloat ("menuVol", out menuVol);

        masterVolSlider.value = masterVol;
        musicVolSlider.value = musicVol;
        effectsVolSlider.value = effectsVol;
        menuVolSlider.value = menuVol;
    }

    public void onVolumeChange ()
    {
        Storage.Save ();
    }

    public void setMasterVol (float value)
    {
        Storage.VolumeMaster = (int)value;
        audioMixer.SetFloat ("masterVol", value);
    }

    public void setMusicVol (float value)
    {
        Storage.VolumeMusic = (int)value;
        audioMixer.SetFloat ("musicVol", value);
    }

    public void setEffectsVol (float value)
    {
        Storage.VolumEffects = (int)value;
        audioMixer.SetFloat ("effectsVol", value);
    }

    public void setMenuVol (float value)
    {
        Storage.VolumeMenu = (int)value;
        audioMixer.SetFloat ("menuVol", value);
    }
}
