using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct Timeframe
{
    public int hours;
    public int minutes;

    public Timeframe(int pH, int pM)
    {
        hours = pH;
        minutes = pM;
    }

    public override string ToString()
    {
        return string.Format("{0:D2}:{1:D2}", hours, minutes);
    }

    public bool Equal(Timeframe time)
    {
        if(hours == time.hours && minutes == time.minutes)
        {
            return true;
        }

        return false;
    }
}




