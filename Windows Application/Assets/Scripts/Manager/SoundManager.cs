using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    [SerializeField] EventReference mainMenuSound;
    [SerializeField] EventReference ambientSound;
    SoundSystem backgroundSystem;

    [SerializeField] EventReference bossCall;
    SoundSystem bossSystem;

    [SerializeField] PlayableDirector[] lightTimeLines = new PlayableDirector[3];

    int lightStage;

    void Start()
    {
        lightStage = 0;

        backgroundSystem = gameObject.AddComponent<SoundSystem>();
        backgroundSystem.SetSoundValues(mainMenuSound);
        backgroundSystem.PlaySound();

        bossSystem = gameObject.AddComponent<SoundSystem>();
        bossSystem.SetSoundValues(bossCall);

        EventBus<GameStartEvent>.OnEvent += StartSound;
        EventBus<LightFlickerEvent>.OnEvent += LightFlickerVersion;
        lightTimeLines[0].stopped += SetAmbientParameter;
    }

    void OnDestroy()
    {
        EventBus<GameStartEvent>.OnEvent -= StartSound;
        EventBus<LightFlickerEvent>.OnEvent -= LightFlickerVersion;
        lightTimeLines[0].stopped -= SetAmbientParameter;
    }

    void StartSound(GameStartEvent gameStartEvent)
    {
        backgroundSystem.StopSound();
        backgroundSystem.SetSoundValues(ambientSound);
        backgroundSystem.PlaySound();
        bossSystem.PlaySound();
    }

    void LightFlickerVersion(LightFlickerEvent lightFlickerEvent)
    {
        if (lightStage > 2) lightStage = 0;

        lightTimeLines[lightStage].Play();
    }

    void SetAmbientParameter(PlayableDirector director)
    {
        backgroundSystem.SetParameterLocal("Amb Progression", 2);
    }
}
