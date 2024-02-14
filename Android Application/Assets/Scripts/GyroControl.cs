using System.Collections;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;
    float offsetZ;
    Quaternion rot;


    // Start is called before the first frame update
    void Start()
    {

        gyroEnabled = enableGyro();
        if (gyroEnabled)
        {
            StartCoroutine(setOffsetZ());
        }


    }

    void Update()
    {
        if (gyroEnabled) {
            rot = gyro.attitude * new Quaternion(0, 0, 1, 0);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.red);
        }

        if(rot.eulerAngles.z<45 || rot.eulerAngles.z> 315)
        {
            transform.rotation = Quaternion.Euler(rot.eulerAngles.x * 2, -rot.eulerAngles.z * 2 + offsetZ * 2, rot.eulerAngles.y * 2);
            //Debug.Log("South");
        }
        if(rot.eulerAngles.z > 45 && rot.eulerAngles.z < 135)
        {
            transform.rotation = Quaternion.Euler(rot.eulerAngles.y * 2, -rot.eulerAngles.z * 2 + offsetZ * 2, rot.eulerAngles.x * 2);
            //Debug.Log("East");
        }
        if (rot.eulerAngles.z > 135 && rot.eulerAngles.z < 225)
        {
            transform.rotation = Quaternion.Euler(-rot.eulerAngles.x * 2, -rot.eulerAngles.z * 2 + offsetZ * 2, rot.eulerAngles.y * 2);
           // Debug.Log("North");
        }
        if (rot.eulerAngles.z > 225 && rot.eulerAngles.z < 315)
        {
            transform.rotation = Quaternion.Euler(-rot.eulerAngles.y* 2, -rot.eulerAngles.z * 2 + offsetZ * 2, rot.eulerAngles.x * 2);
            //Debug.Log("West");
        }
    }

    private bool enableGyro()
    {
        if (SystemInfo.supportsGyroscope )
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        Debug.Log("gyro missing");
        return false;
    } 

    private IEnumerator setOffsetZ()
    {
        rot = gyro.attitude * new Quaternion(0, 0, 1, 0);
        yield return new WaitForSeconds(0.6f);
        offsetZ = rot.eulerAngles.z;
        Debug.Log("set");
    }

    public void ReCalibrate()
    {
        if (!gyroEnabled)
        {
            gyroEnabled = enableGyro();
        }
        StartCoroutine(setOffsetZ());
    }
}
