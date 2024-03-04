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
                    int amountOfMonsters = GameObject.FindGameObjectsWithTag("Monster").Count();
                    if (amountOfMonsters > 0)
                    {
                        int r = Random.Range(0, amountOfMonsters);
                        EventBus<MonsterEvent>.Publish(new MonsterEvent(r));
                    }
                    else Debug.Log("EventTimer: No monsters in scene...");

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
            case GameEvents.LightFlicker: 
                {
                    EventBus<LightFlickerEvent>.Publish(new LightFlickerEvent());
                    break;
                }
            case GameEvents.LightOnOff:
                {
                    EventBus<LightTurnOnOffEvent>.Publish(new LightTurnOnOffEvent());
                    break;
                }
        }
    }
}