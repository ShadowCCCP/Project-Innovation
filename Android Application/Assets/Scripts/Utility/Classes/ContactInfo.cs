using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactInfo : MonoBehaviour
{
    // This script needs to be attached to a gameObject that has a SoundSystem script component...

    public enum Answers { Yes, No }

    [SerializeField] string contactName;
    [SerializeField] Answers[] rightAnswers = new Answers[3];

    List<Answers> givenAnswers = new List<Answers>();

    int questionAmount;
    int currentStage;
    bool isDead;

    private void Start()
    {
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
        UDPSender.SendBroadcast("Contact: " + contactName + ", " + currentStage);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
