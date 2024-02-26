using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public float radius = 1f;
    public float maxDistance = 10f;
    public string targetTag = "Monster";

    void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.right, maxDistance);
        DrawSphereCast(transform.position, transform.right, radius, maxDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag(targetTag))
            {
                Debug.Log("Hit " + targetTag + " at: " + hit.point);
            }
        }
    }

    void DrawSphereCast(Vector3 origin, Vector3 direction, float radius, float maxDistance)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawLine(origin, origin + direction * maxDistance);
    }
}
