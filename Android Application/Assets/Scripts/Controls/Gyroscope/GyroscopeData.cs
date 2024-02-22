using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeData : MonoBehaviour
{
    Quaternion startRotation;
    Gyroscope gyro;

    [Range(5.0f, 25.0f)]
    [SerializeField] float rotationSpeed = 50.0f;

    private void Start()
    {
        Input.simulateMouseWithTouches = true;
        if(EnableGyro())
        {
            // Save inversed starting rotation and add 90 degrees to x-axis to correct starting position...
            startRotation = Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(gyro.attitude);
        }
    }

    void Update()
    {
        if (gyro == null) return;

        ResetOrientation();
        Quaternion raw = gyro.attitude;

        // Make the rotation relative to the current rotation rather than the world...
        Quaternion rot = startRotation * raw;

        // Adjust the rotation to match Unity's coordinate system...
        rot = new Quaternion(-rot.x, rot.y, -rot.z, rot.w);

        Vector3 up = rot * Vector3.up;
        Vector3 forward = rot * Vector3.forward;

        UDPSender.SendBroadcast("Gyroscope: Forward" + forward + " Up" + up);

        // Make the object face the updated up and forward directions...
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward, up), rotationSpeed * Time.deltaTime);
    }

    void ResetOrientation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startRotation = Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(gyro.attitude);
        }
    }

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        Debug.Log("Gyroscope not supported");
        return false;
    }
}