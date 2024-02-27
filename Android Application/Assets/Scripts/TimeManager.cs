using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{
    static float timeInSeconds=0;

    [SerializeField] float gameLengthTimeSeconds = 480;
    [SerializeField] float fifteenMinutesInSeconds = 1;

    void Start()
    {
        EventBus<StartGameEvent>.OnEvent += StartGame;
    }

    void OnDestroy()
    {
        EventBus<StartGameEvent>.OnEvent -= StartGame;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnGameOver()
    {
        UIManager.Instance.ShowGameOver();
    }

    public void StartGame(StartGameEvent startGameEvent)
    {
        //start time
        StartCoroutine(timeCoroutine());
    }

    IEnumerator timeCoroutine()
    {
        while (timeInSeconds < gameLengthTimeSeconds)
        {
            yield return new WaitForSeconds(fifteenMinutesInSeconds);
            timeInSeconds += 15;

            
            UIManager.Instance.UpdateClock(formatHour(), formatMinute());
        }

    }

    static string formatHour()
    {
        Debug.Log("Hours: " + ((int)timeInSeconds / 60 - 2));
        int hour = (int)timeInSeconds / 60 - 2;
        if (hour < 0)
        {
            hour = 12 + hour;
        }
        return hour.ToString();
    }

    static string formatMinute()
    {
        Debug.Log("Minutes: " + (int)timeInSeconds % 60);
        string minuteString = ((int)timeInSeconds % 60).ToString();

        if (minuteString == "0")
        {
            minuteString = "00";
        }
        return minuteString;
    }

    public static string GetCurrentTimeString()
    {
        return formatHour() + ":" + formatMinute();
    }

}