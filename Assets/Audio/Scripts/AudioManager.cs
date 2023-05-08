using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance AmbienceEventInstance;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found More than one Audio Manager in the scene");
        }
        instance= this;

        eventInstances= new List<EventInstance>();
    }
    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
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

    public void SetAmbienceParameter(string ParamaterName, float parameterValue)
    {
        AmbienceEventInstance.setParameterByName(ParamaterName, parameterValue);
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
