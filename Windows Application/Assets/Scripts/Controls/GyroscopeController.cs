using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    [Range(5.0f, 25.0f)]
    [SerializeField] float rotationSpeed = 8.0f;

    Vector3 forward;
    Vector3 up;

    void Awake()
    {
        EventBus<GyroscopeEvent>.OnEvent += RotateObject;
    }

    void Start()
    {
        forward = Vector3.zero;
        up = Vector3.zero;
    }

    void OnDestroy()
    {
        EventBus<GyroscopeEvent>.OnEvent -= RotateObject;
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward, up), rotationSpeed * Time.deltaTime);
    }

    void RotateObject(GyroscopeEvent gyroEvent)
    {
        Debug.Log(gyroEvent.forward);
        forward = gyroEvent.forward;
        up = gyroEvent.up;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(gyroEvent.forward, gyroEvent.up), rotationSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(gyroEvent.forward, gyroEvent.up);
    }
}