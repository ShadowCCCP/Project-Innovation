using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    bool packageTesting;

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

    void PackageTest()
    {
        Debug.Log("Second " + i + " huhu");
    }
}
