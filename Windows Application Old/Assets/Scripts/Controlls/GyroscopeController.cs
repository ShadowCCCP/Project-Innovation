using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    
    void Awake()
    {
        EventBus<GyroscopeEvent>.OnEvent += RotateObject;
    }

    void OnDestroy()
    {
        EventBus<GyroscopeEvent>.OnEvent -= RotateObject;
    }

    void RotateObject(GyroscopeEvent gyroEvent)
    {
        transform.localRotation = Quaternion.Euler(gyroEvent.rotation.X, gyroEvent.rotation.Y, gyroEvent.rotation.Z);
    }
}

// 7087
// 8089
