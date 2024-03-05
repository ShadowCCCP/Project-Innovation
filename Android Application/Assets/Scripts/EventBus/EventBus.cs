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
    public MessageEvent() { }
}
public class CallEvent : Event
{
    public CallEvent() { }
}
public class StartGameEvent : Event
{
    public StartGameEvent() { }
}
public class GameRestartEvent : Event
{
    public GameRestartEvent() { }
}
public class GameOverEvent : Event
{
    public GameOverEvent() { }
}
public class GameWonEvent : Event
{
    public GameWonEvent() { }
}