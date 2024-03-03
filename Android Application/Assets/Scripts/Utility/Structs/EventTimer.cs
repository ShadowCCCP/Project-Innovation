using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct EventTimer
{
    public enum GameEvents { Monster, Message, Call , LightFlicker, LightOnOff}

    public Timeframe time;
    public GameEvents gameEvent;

    public void TriggerEvent()
    {
        switch (gameEvent) 
        {
            case GameEvents.Monster:
                {
                    UDPSender.SendBroadcast("Monster");
                    break;
                }
            case GameEvents.Message:
                {
                    EventBus<MessageEvent>.Publish(new MessageEvent());
                    break;
                }
            case GameEvents.Call:
                {
                    EventBus<CallEvent>.Publish(new CallEvent());
                    break;
                }
            case GameEvents.LightFlicker: 
                {
                    UDPSender.SendBroadcast("LightFlicker");
                    break;
                }
            case GameEvents.LightOnOff:
                {
                    UDPSender.SendBroadcast("LightOnOff");
                    break;
                }
        }
    }
}