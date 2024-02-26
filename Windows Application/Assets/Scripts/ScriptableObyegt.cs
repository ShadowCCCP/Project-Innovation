using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScriptableObyegt
{
    public enum GameEvents { Monster, Message }

    public int time;
    public GameEvents gameEvent;
}
