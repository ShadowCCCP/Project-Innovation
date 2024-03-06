using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCastLockAcquirer : MonoBehaviour
{
    public float multicastDelay = 1f;
    private bool stopAcquiringLock = false;

    void Start()
    {
        //StartCoroutine(AcquireMulticastPeriodically());
    }

    IEnumerator AcquireMulticastPeriodically()
    {
        while (!stopAcquiringLock)
        {
            GetMulticastLock("debugMulticast");
            yield return new WaitForSeconds(multicastDelay);
        }
    }

    void OnDestroy()
    {
        stopAcquiringLock = true; // Ensure coroutine stops when the object is destroyed
    }

    bool GetMulticastLock(string lockTag)
    {
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
            {
                AndroidJavaObject multicastLock = wifiManager.Call<AndroidJavaObject>("createMulticastLock", lockTag);
                multicastLock.Call("acquire");
                bool isHeld = multicastLock.Call<bool>("isHeld");
                return isHeld;
            }
        }
    }
}
