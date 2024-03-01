using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactInfo : MonoBehaviour
{
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
        soundSystem = new SoundSystem(soundReference, true);
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
