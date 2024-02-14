using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall : MonoBehaviour
{
    public string soundEvent; // FMOD sound event path

    private FMOD.Studio.EventInstance soundInstance;

    void Update()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        soundInstance.getPlaybackState(out playbackState);
        if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            NetworkSender.SendBroadcast("Windows: Phone Call end");
        }
    }
}
