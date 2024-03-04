using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactInfo : MonoBehaviour
{
    // This script needs to be attached to a gameObject that has a SoundSystem script component...

    public enum Answers { Yes, No }

    [SerializeField] string contactName;
    [SerializeField] EventReference soundReference;
    [SerializeField] Answers[] rightAnswers = new Answers[3];

    List<Answers> givenAnswers = new List<Answers>();
    SoundSystem soundSystem;

    int questionAmount;
    int currentStage;
    bool isDead;

    private void Start()
    {
        soundSystem = GetComponent<SoundSystem>();
        if (soundSystem == null)
        {
            soundSystem = gameObject.AddComponent<SoundSystem>();
        }
        soundSystem.SetSoundValues(soundReference, true);

        questionAmount = rightAnswers.Length;
    }

    public void AddAnswer(Answers answer)
    {
        givenAnswers.Add(answer);
        CheckAnswers();
    }

    void CheckAnswers()
    {
        if (givenAnswers.Count == questionAmount)
        {
            for (int i = 0; i < questionAmount; i++) 
            { 
                if (givenAnswers[i] != rightAnswers[i])
                {
                    isDead = true;
                }
            }
        }

        IncreaseStage();
    }

    void IncreaseStage()
    {
        currentStage++;
        if (isDead) currentStage++;
        soundSystem.SetParameter(SoundSystem.Parameters.X, currentStage);
    }
}
