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
public class TimeEvent : Event
{
    public TimeEvent(Timeframe pTime)
    {
        time = pTime;
    }

    public Timeframe time;
}

public class MonsterEvent : Event
{
    public MonsterEvent(int pMonsterID)
    {
        monsterID = pMonsterID;
    }

    public int monsterID;
}

public class CallStageEvent : Event
{
    public CallStageEvent(string pContactName, int pStage)
    {
        contactName = pContactName;
        stage = pStage;
    }

    public string contactName;
    public int stage;
}
public class LightFlickerEvent : Event
{
    public LightFlickerEvent()
    {

    }

}
public class LightTurnOnOffEvent : Event
{
    public LightTurnOnOffEvent()
    {

    }

}

public class GameRestartedEvent : Event
{
    public GameRestartedEvent()
    {

    }

}

public class GameOverEvent : Event
{
    public GameOverEvent(GameManager.GameOverType pGameOverType)
    {
        gameOverType = pGameOverType;
    }
    public GameManager.GameOverType gameOverType;
}

public class GameStartEvent : Event
{
    public GameStartEvent() { }
}

public class FlashlightOnOffEvent : Event
{
    public FlashlightOnOffEvent()
    {

    }

}

