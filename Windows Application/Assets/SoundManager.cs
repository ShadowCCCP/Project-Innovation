using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] EventReference ambientSound;
    SoundSystem ambientSystem;

    [SerializeField] EventReference bossCall;
    SoundSystem bossSystem;

    int lightStage;

    void Start()
    {
        lightStage = 0;

        ambientSystem = gameObject.AddComponent<SoundSystem>();
        ambientSystem.SetSoundValues(ambientSound);

        bossSystem = gameObject.AddComponent<SoundSystem>();
        bossSystem.SetSoundValues(bossCall);

        EventBus<GameStartEvent>.OnEvent += StartSound;
    }

    void OnDestroy()
    {
        EventBus<GameStartEvent>.OnEvent -= StartSound; 
    }

    void StartSound(GameStartEvent gameStartEvent)
    {
        bossSystem.PlaySound();
    }

    void LightFlickerVersion()
    {
        switch (lightStage)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
