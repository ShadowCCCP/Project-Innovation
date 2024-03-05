using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Contact", menuName = "Contact")]
public class Contact : ScriptableObject
{
    public enum Answers { Yes, No }

    public string contactName;
    public string details;
    public Sprite icon;

    public Answers[] rightAnswers = new Answers[3];
    List<Answers> givenAnswers = new List<Answers>();

    int currentStage;
    bool isDead;

    public void StartCall()
    {
        UDPSender.SendBroadcast("Contact: " + contactName + ", " + currentStage);
    }

    public void AddAnswer(Answers answer)
    {
        givenAnswers.Add(answer);
        CheckAnswers();
    }

    void CheckAnswers()
    {
        if (givenAnswers.Count == rightAnswers.Length)
        {
            for (int i = 0; i < rightAnswers.Length; i++)
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
