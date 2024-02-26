using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct EventTimer
{
    public enum GameEvents { Monster, Message }

    public Timeframe time;
    public GameEvents gameEvent;
}