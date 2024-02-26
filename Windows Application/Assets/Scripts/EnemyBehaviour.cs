using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    List<Transform> navPoints = new List<Transform>();
    Quaternion previousRotation;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i< navPoints.Count; i++)
        {
            navPoints[i].LookAt(transform);
        }
        transform.LookAt(navPoints[i].position);
    }

    int i =0;
    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.Translate((navPoints[i].position - transform.position).normalized * speed * Time.deltaTime);
        if (speed > 0)
        {
            if (i < navPoints.Count)
            {
                if (Vector3.Distance(transform.position, navPoints[i].position) < 0.1f)
                {
                    navPoints[i].rotation = transform.rotation;
                    i++;
                    if(i < navPoints.Count) transform.LookAt(navPoints[i].position);
                    

                }
            }
            else
            {
                speed = 0;
            }
        }
        else if (speed <0)
        {
            if (i >=-10)
            {
                Debug.Log(Vector3.Distance(transform.position, navPoints[i - 1].position) < 0.1f);
                if (Vector3.Distance(transform.position, navPoints[i-1].position) < 0.1f)
                {
                    
                    transform.rotation = navPoints[i-1].rotation;
                    i--;               

                }
            }
            else
            {
                speed = 0;
            }
        }
        
    }



}
