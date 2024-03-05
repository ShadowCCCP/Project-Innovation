using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static ContactInfo;

public class MessageHandler : MonoBehaviour
{
    [SerializeField] bool packageTesting;

    int i = 0;
    float timer;

    int monstersCount;

    void Update()
    {
        if (packageTesting)
        {
            timer += Time.deltaTime;
            int seconds = (int)timer % 60;
            i = seconds;
        }
    }

    public void ProcessMessage(string message)
    {
        // For network testing...
        if (packageTesting) PackageTest();

        if (message.Contains("Gyroscope")) GyroscopeData(message);
        else if (message.Contains("Time")) TimeData(message);
        else if (message.Contains("Contact")) CallStateData(message);
        else if (message.Contains("LightFlicker")) LightFlickerData();
        else if (message.Contains("LightOnOff")) LightOnOffData();
        else if (message.Contains("Restart")) RestartGame();
        else if (message.Contains("Over")) GameOver();
        else if (message.Contains("Won")) GameWon();
        else if (message.Contains("StartGame")) StartGameData();
        else if (message.Contains("Monster")) SpawnMonster();
    }

    void GyroscopeData(string message)
    {
        Vector3 forward = StringHandler.ExtractGyroVector(message, "Forward");
        Vector3 up = StringHandler.ExtractGyroVector(message, "Up");

        EventBus<GyroscopeEvent>.Publish(new GyroscopeEvent(forward, up));
    }

    void TimeData(string message)
    {
        Timeframe time = StringHandler.ExtractTime(message);
        EventBus<TimeEvent>.Publish(new TimeEvent(time));
    }

    void CallStateData(string message)
    {
        string contactName = StringHandler.ExtractCallStage(message, false);
        int stage = int.Parse(StringHandler.ExtractCallStage(message, true));
        EventBus<CallStageEvent>.Publish(new CallStageEvent(contactName, stage));
    }

    void LightFlickerData()
    {
        EventBus<LightFlickerEvent>.Publish(new LightFlickerEvent());
    }

    void LightOnOffData()
    {
        EventBus<LightTurnOnOffEvent>.Publish(new LightTurnOnOffEvent());
    }

    void RestartGame()
    {
        EventBus<GameRestartedEvent>.Publish(new GameRestartedEvent());
    }

    void GameOver()
    {
        EventBus<GameOverEvent>.Publish(new GameOverEvent(GameManager.GameOverType.DeadResident));
    }

    void GameWon()
    {
        EventBus<GameOverEvent>.Publish(new GameOverEvent(GameManager.GameOverType.Won));
    }

    void StartGameData()
    {
        EventBus<GameStartEvent>.Publish(new GameStartEvent());
    }

    void SpawnMonster()
    {
        int monster = FindObjectsOfType<EnemyBehaviour>().Length;
        EventBus<MonsterEvent>.Publish(new MonsterEvent(monster));
    }

    void PackageTest()
    {
        Debug.Log("Second " + i + " huhu");
    }
}
