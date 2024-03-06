using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactInfo : MonoBehaviour
{
    [SerializeField] string contactName;

    [SerializeField] EventReference soundReference;
    SoundSystem soundSystem;

    void Start()
    {
        // Get or create soundSystem component and set soundReference inside...
        soundSystem = GetComponent<SoundSystem>();
        if (soundSystem == null)
        {
            soundSystem = gameObject.AddComponent<SoundSystem>();
        }
        soundSystem.SetSoundValues(soundReference, true);

        EventBus<CallStageEvent>.OnEvent += StageCall;
    }

    void OnDestroy()
    {
        EventBus<CallStageEvent>.OnEvent -= StageCall;
    }

    void StageCall(CallStageEvent callStageEvent)
    {
        if (CompareName(callStageEvent.contactName))
        {
            PlayCall(callStageEvent.stage);
        }
    }

    bool CompareName(string name)
    {
        if (name == contactName) return true;
        return false;
    }

    void PlayCall(int stage)
    {
        if (!soundSystem.IsPlaying()) soundSystem.PlaySound();
        soundSystem.SetParameterGlobal("Calling", stage);
    }
}
