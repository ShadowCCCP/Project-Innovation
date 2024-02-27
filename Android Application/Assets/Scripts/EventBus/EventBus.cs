using System;
using UnityEngine;

public abstract class Event { }

public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public class MessageEvent : Event
{
    public MessageEvent()
    {
    }
}

public class CallEvent : Event
{
    public CallEvent()
    {
    }
}