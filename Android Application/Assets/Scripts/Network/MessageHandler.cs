using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MessageHandler : MonoBehaviour
{
    public void ProcessMessage(string message)
    {
        if (message.Contains("Message")) TriggerMessage();
        else if (message.Contains("Call")) TriggerCall();
    }

    void TriggerMessage()
    {
        EventBus<MessageEvent>.Publish(new MessageEvent());
    }

    void TriggerCall()
    {
        EventBus<CallEvent>.Publish(new CallEvent());
    }
}
