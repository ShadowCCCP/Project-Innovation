using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{

    [SerializeField] Timeframe startTime;
    [SerializeField] int totalGameLengthInSeconds = 480;
    [SerializeField] float fifteenMinutesInSeconds = 1;
    [SerializeField] bool analTimeFormat;

    int startMinutes;
    int currentMinutes;
    public static string currentTime; 

    void Start()
    {
        EventBus<StartGameEvent>.OnEvent += StartGame;

        currentMinutes = startTime.hours * 60 + startTime.minutes;
        startMinutes = currentMinutes;

        UIManager.Instance.UpdateClock(startTime.hours.ToString(), startTime.minutes.ToString("00"));
        currentTime = GetCurrentTimeString();
    }

    void OnDestroy()
    {
        EventBus<StartGameEvent>.OnEvent -= StartGame;
    }

    void Update()
    {
        //Debug.Log(currentMinutes);
    }

    public void StartGame(StartGameEvent startGameEvent)
    {
        InvokeRepeating("UpdateTime", fifteenMinutesInSeconds, fifteenMinutesInSeconds);
    }

    void UpdateTime()
    {
        currentMinutes += 15;

        Timeframe time = GetCurrentTime();

        UIManager.Instance.UpdateClock(time.hours.ToString(), time.minutes.ToString("00"));
        currentTime = GetCurrentTimeString();

        //if (currentTime.hours > 23) currentMinutes -= 60 * 24;

        // If time limit is succeeded, reset it back to the startTime....
        if (currentMinutes >= startMinutes + totalGameLengthInSeconds)
        {
            // Game Won?
            currentMinutes = startMinutes;
        }
    }

    string GetCurrentTimeString()
    {
        if (analTimeFormat) return AnalTimeFormat();
        else return DigitalTimeFormat();
    }

    string AnalTimeFormat()
    {
        // Analogous time format...
        Timeframe currentTime = GetCurrentTime();
        Debug.Log(currentTime.hours);

        if (currentTime.hours == 0) currentTime.hours = 12;
        else if (currentTime.hours > 12) currentTime.hours -= 12;

        return currentTime.hours + ":" + currentTime.minutes.ToString("00");
    }

    string DigitalTimeFormat()
    {
        // Digital time format...
        Timeframe currentTime = GetCurrentTime();

        return currentTime.hours + ":" + currentTime.minutes.ToString("00");
    }

    Timeframe GetCurrentTime()
    {
        return new Timeframe(currentMinutes / 60, currentMinutes % 60);
    }
}