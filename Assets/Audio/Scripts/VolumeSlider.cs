using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType { Master,Music,Ambiece,SFX}

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        switch(volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.Music:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.Ambiece:
                volumeSlider.value = AudioManager.instance.ambienceVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;

                default:
                    Debug.LogWarning("Volume Type not supported " + volumeType);
                break;
        }
    }
    public void OnsliderValueChange()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.Music:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.Ambiece:
                AudioManager.instance.ambienceVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;

            default:
                Debug.LogWarning("Volume Type not supported " + volumeType);
                break;
        }
    }
}
