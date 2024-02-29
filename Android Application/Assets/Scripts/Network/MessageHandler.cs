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

        if (message.Contains("Message")) PhoneMessageData();
        else if (message.Contains("Call")) PhoneCallData();
    }

    void PhoneMessageData()
    {
        EventBus<MessageEvent>.Publish(new MessageEvent());
    }

    void PhoneCallData()
    {
        EventBus<CallEvent>.Publish(new CallEvent());
    }

    void PackageTest()
    {
        Debug.Log("Second " + i + " huhu");
    }
}
