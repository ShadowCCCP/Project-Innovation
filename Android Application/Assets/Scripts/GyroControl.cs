using System.Collections;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;
    float offsetZ;
    Quaternion rot;
    bool offsetSet;
    // Start is called before the first frame update
    void Start()
    {

        gyroEnabled = enableGyro();
        rot = gyro.attitude * new Quaternion(0, 0, 1, 0);
        offsetZ = 0;
        StartCoroutine(SetOffsetZ());
    }

    void Update()
    {
        if (gyroEnabled) {
            rot = gyro.attitude * new Quaternion(0, 0, 1, 0);
            //Debug.Log(-rot.eulerAngles.z+ offsetZ);
            // transform.rotation = Quaternion.Euler(0, Mathf.Clamp(-rot.eulerAngles.z+90 + offsetX, -90, 90)-90, 0);
            transform.rotation = Quaternion.Euler(rot.eulerAngles.x, -rot.eulerAngles.z + offsetZ, rot.eulerAngles.y);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.red);
        }
    }

    private bool enableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    } 

    private IEnumerator SetOffsetZ()
    {
        yield return new WaitForSeconds(0.6f);
        offsetZ = rot.eulerAngles.z;
        offsetSet = true;
        Debug.Log("set");
    }
}
