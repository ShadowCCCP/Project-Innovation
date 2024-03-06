using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] EventReference eventRef;
    [SerializeField] bool threeDimensional;

    EventInstance soundInstance;

    PLAYBACK_STATE currentState;

    private void Start()
    {

        // For local parameters...
        //soundInstance.setParameterByID(paramIdX, 1);

        // For global parameters...
        //RuntimeManager.StudioSystem.setParameterByName("...", 1);

    }

    public void SetSoundValues(EventReference pEventRef, bool pThreeDimensional = false)
    {
        eventRef = pEventRef;
        threeDimensional = pThreeDimensional;

        SetupSound();
    }

    void SetupSound()
    {
        soundInstance = RuntimeManager.CreateInstance(eventRef);

        //if (threeDimensional)
        soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    }

    public void SetParameterLocal(string parameterName, int value)
    {
        soundInstance.setParameterByName(parameterName, value);
    }

    public void SetParameterGlobal(string parameterName, int value)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }

    public void PlaySound()
    {
        if(soundInstance.isValid())
        {
            soundInstance.start();
        }
    }

    public bool IsPlaying()
    {
        soundInstance.getPlaybackState(out currentState);
        if(currentState == PLAYBACK_STATE.SUSTAINING) return false;

        return true;
    }
}
