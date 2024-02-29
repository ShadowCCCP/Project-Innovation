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
    [SerializeField] bool analogousTimeFormat;

    int startMinutes;
    int totalMinutes;

    int currentMinutes;
    public static string currentTime;

    void Start()
    {
        EventBus<StartGameEvent>.OnEvent += StartGame;

        // CurrentMinutes to have a time variable to change things properly...
        currentMinutes = startTime.hours * 60 + startTime.minutes;
        startMinutes = currentMinutes;

        // TotalMinutes to read the time passed untouched...
        totalMinutes = currentMinutes;

        currentTime = GetCurrentTimeString();

        DigitalOrAnalogous();
    }

    void OnDestroy()
    {
        EventBus<StartGameEvent>.OnEvent -= StartGame;
    }

    public void StartGame(StartGameEvent startGameEvent)
    {
        InvokeRepeating("UpdateTime", fifteenMinutesInSeconds, fifteenMinutesInSeconds);
    }

    void UpdateTime()
    {
        currentMinutes += 15;
        totalMinutes += 15;

        // If time limit is succeeded, reset it back to the startTime....
        if (totalMinutes >= startMinutes + totalGameLengthInSeconds)
        {
            // Game Won?
            CancelInvoke("UpdateTime");
        }

        DigitalOrAnalogous();
        currentTime = GetCurrentTimeString();

        UDPSender.SendBroadcast("Time: " + DigitalTimeFormat());
    }

    void DigitalOrAnalogous()
    {
        if (analogousTimeFormat) UIManager.Instance.UpdateClock(AnalTimeFormat());
        else UIManager.Instance.UpdateClock(DigitalTimeFormat());
    }


    string GetCurrentTimeString()
    {
        if (analogousTimeFormat) return AnalTimeFormat();
        else return DigitalTimeFormat();
    }

    string AnalTimeFormat()
    {
        // Analogous time format...
        if (currentMinutes / 60 == 0) currentMinutes = 60 * 12;
        else if (currentMinutes / 60 > 12) currentMinutes -= 60 * 12;

        return currentMinutes / 60 + ":" + (currentMinutes % 60).ToString("00");
    }

    string DigitalTimeFormat()
    {
        // Digital time format...
        if (currentMinutes / 60 >= 24) currentMinutes -= 60 * 24;

        return currentMinutes / 60 + ":" + (currentMinutes % 60).ToString("00");
    }
}