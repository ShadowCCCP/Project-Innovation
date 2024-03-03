using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    [SerializeField] bool packageTesting;

    int i = 0;
    float timer;

    void Update()
    {
        if (packageTesting)
        {
            timer += Time.deltaTime;
            int seconds = (int)timer % 60;
            i = seconds;
        }
    }

    public void ProcessMessage(string message)
    {
        // For network testing...
        if (packageTesting) PackageTest();

        if (message.Contains("Gyroscope")) GyroscopeData(message);
        else if (message.Contains("Time")) TimeData(message);
        else if (message.Contains("Answer")) AnswerData(message);
        else if (message.Contains("CallState")) CallStateData(message);
        else if (message.Contains("LightFlicker")) LightFlickerData();
        else if (message.Contains("LightOnOff")) LightOnOffData();
    }

    void GyroscopeData(string message)
    {
        Vector3 forward = StringHandler.ExtractGyroVector(message, "Forward");
        Vector3 up = StringHandler.ExtractGyroVector(message, "Up");

        EventBus<GyroscopeEvent>.Publish(new GyroscopeEvent(forward, up));
    }

    void TimeData(string message)
    {
        Timeframe time = StringHandler.ExtractTime(message);
        EventBus<TimeEvent>.Publish(new TimeEvent(time));
    }

    void AnswerData(string message)
    {
        ContactInfo.Answers answer = ContactInfo.Answers.Yes;
        string answerStr = StringHandler.ExtractAnswer(message);
        switch (answerStr)
        {
            case "Yes":
                answer = ContactInfo.Answers.Yes;
                break;
            case "No":
                answer = ContactInfo.Answers.No;
                break;
        }
        EventBus<AnswerEvent>.Publish(new AnswerEvent(answer));
    }

    void CallStateData(string message)
    {
        string state = StringHandler.ExtractAnswer(message);
        EventBus<CallStateEvent>.Publish(new CallStateEvent(state));
    }

    void LightFlickerData()
    {
        EventBus<LightFlickerEvent>.Publish(new LightFlickerEvent());
    }

    void LightOnOffData()
    {
        EventBus<LightTurnOnOffEvent>.Publish(new LightTurnOnOffEvent());
    }

    void PackageTest()
    {
        Debug.Log("Second " + i + " huhu");
    }
}
