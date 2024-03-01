using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public SoundSystem(EventReference pEventRef, bool pThreeDimensional = false) 
    {
        eventRef = pEventRef;
        threeDimensional = pThreeDimensional;
    }

    public enum Parameters { X, Y };

    [SerializeField] EventReference eventRef;
    [SerializeField] bool threeDimensional;

    EventInstance soundInstance;

    [SerializeField] PARAMETER_ID paramIdX;
    [SerializeField] PARAMETER_ID paramIdY;

    PLAYBACK_STATE currentState;

    private void Start()
    {
        soundInstance = RuntimeManager.CreateInstance(eventRef);

        if (threeDimensional)
        soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        paramIdX = GetParameterID("x");
        paramIdY = GetParameterID("y");
      
        // For local parameters...
        //soundInstance.setParameterByID(paramIdX, 1);

        // For global parameters...
        //RuntimeManager.StudioSystem.setParameterByName("...", 1);

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

    public PARAMETER_ID GetParameterID(string paramName)
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
