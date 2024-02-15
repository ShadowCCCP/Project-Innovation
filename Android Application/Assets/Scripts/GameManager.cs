using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float timeInSeconds=0;

    [SerializeField]
    float gameLengthTimeSeconds = 480;

    [SerializeField]
    float fifteenMinutesInSeconds = 1;

    //from 10pm to 6 am 
    //1 hour is 1 min

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnGameOver()
    {
        UIManager.Instance.ShowGameOver();
    }

    public void StartGame()
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

    string formatHour()
    {
        int hour = (int)timeInSeconds / 60 - 2;
        if (hour < 0)
        {
            hour = 12 + hour;
        }
        return hour.ToString();
    }

    string formatMinute()
    {
        string minuteString = ((int)timeInSeconds % 60).ToString();

        if (minuteString == "0")
        {
            minuteString = "00";
        }
        return minuteString;
    }

    public string GetCurrentTimeFormatted()
    {
        return formatHour() + ":" + formatMinute();
    }

}
