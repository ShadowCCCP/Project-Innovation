using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] EventTimer[] eventList;

    private void Start()
    {
        for (int i = 0; i < eventList.Length; i++)
        {
            /*
            Debug.Log("Hours: " + eventList[i].time.hours);
            Debug.Log("Minutes: " + eventList[i].time.minutes);
            Debug.Log("String: " + eventList[i].time.ToString());
            */
        }
    }
}
