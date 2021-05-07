using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicLevel(float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20f);
    }
    
    public void SetSFXLevel(float sliderVal)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderVal) * 20f);
    }
}
