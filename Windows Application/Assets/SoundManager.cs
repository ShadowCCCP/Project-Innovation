using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundManager : MonoBehaviour
{
    [SerializeField] EventReference ambientSound;
    SoundSystem ambientSystem;

    [SerializeField] EventReference bossCall;
    SoundSystem bossSystem;

    [SerializeField] PlayableDirector[] lightTimeLines = new PlayableDirector[3];

    int lightStage;

    void Start()
    {
        lightStage = 0;

        ambientSystem = gameObject.AddComponent<SoundSystem>();
        ambientSystem.SetSoundValues(ambientSound);

        bossSystem = gameObject.AddComponent<SoundSystem>();
        bossSystem.SetSoundValues(bossCall);

        EventBus<GameStartEvent>.OnEvent += StartSound;
        EventBus<LightFlickerEvent>.OnEvent += LightFlickerVersion;
        lightTimeLines[1].stopped += SetAmbientParameter;
    }

    void OnDestroy()
    {
        EventBus<GameStartEvent>.OnEvent -= StartSound;
        EventBus<LightFlickerEvent>.OnEvent -= LightFlickerVersion;
        lightTimeLines[1].stopped -= SetAmbientParameter;
    }

    void StartSound(GameStartEvent gameStartEvent)
    {
        bossSystem.PlaySound();
    }

    void LightFlickerVersion(LightFlickerEvent lightFlickerEvent)
    {
        if (lightStage > 2) lightStage = 0;

        lightTimeLines[lightStage].Play();
    }

    void SetAmbientParameter(PlayableDirector director)
    {
        ambientSystem.SetParameterLocal("Amb Progression", 2);
    }
}
