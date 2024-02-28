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

public class GyroscopeEvent : Event
{
    public GyroscopeEvent(Vector3 pForward, Vector3 pUp)
    {
        forward = pForward;
        up = pUp;
    }

    public Vector3 forward;
    public Vector3 up;
}

public class MonsterEvent : Event
{
    public MonsterEvent(int pMonsterID)
    {
        monsterID = pMonsterID;
    }

    public int monsterID;
}
