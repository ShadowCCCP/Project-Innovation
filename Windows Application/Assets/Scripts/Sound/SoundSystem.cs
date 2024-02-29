using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] EventReference eventRef;

    EventInstance soundInstance;

    [SerializeField] PARAMETER_ID paramIdX;
    [SerializeField] PARAMETER_ID paramIdY;

    PLAYBACK_STATE currentState;

    private void Start()
    {
        soundInstance = RuntimeManager.CreateInstance(eventRef);
        soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        paramIdX = GetParameterID("x");
        paramIdY = GetParameterID("y");

        // For local parameters...
        //soundInstance.setParameterByID(paramIdX, 1);

        // For global parameters...
        //RuntimeManager.StudioSystem.setParameterByName("...", 1);

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
