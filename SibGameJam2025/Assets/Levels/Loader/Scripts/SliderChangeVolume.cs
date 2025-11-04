using SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderChangeVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource source;
    public AudioClip clip;

    public string volumeParameter = "";

    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();

        if (SaveData.Volumes.ContainsKey(volumeParameter))
        {
            float value = SaveData.Volumes[volumeParameter];
            slider.value = value;
            SetVolume();
        }
    }

    public void SetVolume()
    {
        float value = slider.value;
        float dbVolume = Mathf.Log10(value) * 20;

        if (slider.value < 0.01)
            dbVolume = -80;

        if (!SaveData.Volumes.ContainsKey(volumeParameter))
        {
            SaveData.Volumes.Add(volumeParameter, dbVolume);
        }
        else
        {
            SaveData.Volumes[volumeParameter] = dbVolume;
        }

        if (source)
        {
            if (!source.isPlaying)
                source.PlayOneShot(clip);
        }

        mixer.SetFloat(volumeParameter, dbVolume);
    }
}
