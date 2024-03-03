using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public enum Parameters { X, Y };

    [SerializeField] EventReference eventRef;
    [SerializeField] bool threeDimensional;

    EventInstance soundInstance;

    PARAMETER_ID paramIdX;
    PARAMETER_ID paramIdY;

    PLAYBACK_STATE currentState;

    private void Start()
    {
        SetupSound();
      
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

        if (threeDimensional)
        soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        paramIdX = GetParameterID("x");
        paramIdY = GetParameterID("y");
    }

    public void SetParameter(Parameters p, int value)
    {
        switch (p) 
        { 
            case Parameters.X:
                {
                    soundInstance.setParameterByID(paramIdX, value);
                    break;
                }
            case Parameters.Y:
                {
                    soundInstance.setParameterByID(paramIdY, value);
                    break;
                }
        }
    }

    PARAMETER_ID GetParameterID(string paramName)
    {
        EventDescription eventDescription;
        soundInstance.getDescription(out eventDescription);

        PARAMETER_DESCRIPTION paramDescription;
        eventDescription.getParameterDescriptionByName(paramName, out paramDescription);

        return paramDescription.id;
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
