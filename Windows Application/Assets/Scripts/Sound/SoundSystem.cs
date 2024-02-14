using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField]
    EventReference soundEvent;

    private void Start()
    {
        PlayOneShot();
    }

    public void PlayOneShot()
    {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
