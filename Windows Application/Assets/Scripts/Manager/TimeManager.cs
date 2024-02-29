using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] EventTimer[] eventList;

    void Start()
    {
        EventBus<TimeEvent>.OnEvent += CheckTimings;
    }

    void OnDestroy()
    {
        EventBus<TimeEvent>.OnEvent -= CheckTimings;
    }

    void CheckTimings(TimeEvent timeEvent)
    {
        for (int i = 0; i < eventList.Length; i++)
        {
            if (eventList[i].time.Equal(timeEvent.time))
            {
                eventList[i].TriggerEvent();
            }
        }
    }
}
