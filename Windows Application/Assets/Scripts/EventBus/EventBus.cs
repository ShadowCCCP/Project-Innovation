using System;
using System.Numerics;

public abstract class Event { }

public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);   
    }
}

public class GyroscopeEvent : Event
{
    public GyroscopeEvent(Vector3 pRotation)
    {
        rotation = pRotation;
    }

    public Vector3 rotation;
}