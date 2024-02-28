using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    int i = 0;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        i = seconds;
    }

    public void ProcessMessage(string message)
    {
        PackageTest();
        if (message.Contains("Gyroscope")) GyroscopeData(message);
    }

    void GyroscopeData(string message)
    {
        Vector3 forward = (StringHandler.ExtractGyroVector(message, "Forward"));
        Vector3 up = (StringHandler.ExtractGyroVector(message, "Up"));

        EventBus<GyroscopeEvent>.Publish(new GyroscopeEvent(forward, up));
    }

    void PackageTest()
    {
        Debug.Log("Second " + i + " huhu");
    }
}
