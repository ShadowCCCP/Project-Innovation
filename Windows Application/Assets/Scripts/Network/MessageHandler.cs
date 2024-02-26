using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    public void ProcessMessage(string message)
    {
        if (message.Contains("Gyroscope")) GyroscopeData(message);
    }

    void GyroscopeData(string message)
    {
        Vector3 forward = (StringHandler.ExtractGyroVector(message, "Forward"));
        Vector3 up = (StringHandler.ExtractGyroVector(message, "Up"));

        EventBus<GyroscopeEvent>.Publish(new GyroscopeEvent(forward, up));
    }
}
