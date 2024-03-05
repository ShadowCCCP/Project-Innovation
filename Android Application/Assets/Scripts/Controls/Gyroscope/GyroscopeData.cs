using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeData : MonoBehaviour
{
    Quaternion startRotation;
    Gyroscope gyro;

    bool pointingUp;

    private void Start()
    {
        if(EnableGyro())
        {
            // Save inversed starting rotation and add 90 degrees to x-axis to correct starting position...
            ResetOrientation();
        }
    }

    void Update()
    {
        if (gyro == null) return;
        Input.simulateMouseWithTouches = true;

        Quaternion raw = gyro.attitude;

        // Make the rotation relative to the current rotation rather than the world...
        Quaternion rot = startRotation * raw;

        // Adjust the rotation to match Unity's coordinate system...
        if (pointingUp) rot = new Quaternion(-rot.x, -rot.y, -rot.z, rot.w);
        else rot = new Quaternion(-rot.x, rot.y, -rot.z, rot.w);

        Vector3 up = rot * Vector3.up;
        Vector3 forward = rot * Vector3.forward;

        UDPSender.SendBroadcast("Gyroscope: Forward" + forward + " Up" + up);
    }

    public void ResetOrientation()
    {
        startRotation = Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(gyro.attitude);

        // To check if phone is pointing up...
        Vector3 gravity = Input.gyro.gravity;

        if (gravity.y < -0.85) pointingUp = true;
        else pointingUp = false;
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