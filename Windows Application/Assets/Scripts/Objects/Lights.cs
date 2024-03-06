using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Lights : MonoBehaviour
{
    [SerializeField] PlayableDirector lightsTimeLine;

    void Awake()
    {
        EventBus<LightFlickerEvent>.OnEvent += flickerLights; 
    }
    void OnDestroy()
    {
        EventBus<LightFlickerEvent>.OnEvent -= flickerLights;
    }

    void flickerLights(LightFlickerEvent flickerEvent)
    {
        lightsTimeLine.Play();
    }
}
