using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0,1)]

    public float masterVolume = 1.0f;
    [Range(0, 1)]
    public float musicVolume = 1.0f;
    [Range(0, 1)]
    public float ambienceVolume = 1.0f;
    [Range(0, 1)]
    public float SFXVolume = 1.0f;
    [Range(0, 1)]

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;


    private List<EventInstance> eventInstances;

    private EventInstance AmbienceEventInstance;
    private EventInstance MusicEventInstance;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found More than one Audio Manager in the scene");
        }
        instance= this;

        eventInstances= new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");

    }
    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
        InitializeMusic(FMODEvents.instance.Music);
    }
    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        AmbienceEventInstance = CreateEventInstance(ambienceEventReference);
        AmbienceEventInstance.start();
    }
    private void InitializeMusic(EventReference MusicEventReference)
    {
        MusicEventInstance = CreateEventInstance(MusicEventReference);
        MusicEventInstance.start();
    }

    public void SetAmbienceParameter(string ParamaterName, float parameterValue)
    {
        AmbienceEventInstance.setParameterByName(ParamaterName, parameterValue);
    }
    
    public void SetMusicArea(MusicArea area)
    {
        MusicEventInstance.setParameterByName("area", (float)area);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleaUp()
    {
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();

        }
    }
    private void OnDestroy()
    {
        CleaUp();
    }
}
