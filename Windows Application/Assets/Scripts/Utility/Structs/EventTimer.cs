using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct EventTimer
{
    public enum GameEvents { Monster, Message, Call }

    public Timeframe time;
    public GameEvents gameEvent;

    public void TriggerEvent()
    {
        switch (gameEvent) 
        {
            case GameEvents.Monster:
                {
                    EventBus<MonsterEvent>.Publish(new MonsterEvent("placeholder"));
                    break;
                }
            case GameEvents.Message:
                {
                    UDPSender.SendBroadcast("Message");
                    break;
                }
            case GameEvents.Call:
                {
                    UDPSender.SendBroadcast("Call");
                    break;
                }
        }
    }
}