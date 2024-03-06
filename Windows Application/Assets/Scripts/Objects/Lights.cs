using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Lights : MonoBehaviour
{
    [SerializeField]
    Animator lightsFlickering;
    [SerializeField]
    PlayableDirector timeLineLightsOnOff;

    void Awake()
    {
        EventBus<LightFlickerEvent>.OnEvent += flickerLights; 
        EventBus<LightTurnOnOffEvent>.OnEvent += turnLightsOnOff;
    }
    void OnDestroy()
    {
        EventBus<LightFlickerEvent>.OnEvent -= flickerLights;
        EventBus<LightTurnOnOffEvent>.OnEvent -= turnLightsOnOff;
    }

    void flickerLights(LightFlickerEvent flickerEvent)
    {
        if (lightsFlickering)
        {
            lightsFlickering.SetTrigger("StartAnimation");
        }
    }

    void turnLightsOnOff(LightTurnOnOffEvent onOffEvent) 
    {
        timeLineLightsOnOff.Play();
    }
}
