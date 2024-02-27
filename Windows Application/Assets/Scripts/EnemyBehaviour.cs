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

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.LookAt(navPoints[i].position);
        anim.SetFloat("Speed", speed);
    }

    int i =0;
    void Update()
    {
        anim.SetFloat("Speed", speed);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (speed > 0)
        {
            if (i < navPoints.Count)
            {
                if (Vector3.Distance(transform.position, navPoints[i].position) < 0.1f)
                {
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
            if (i >0)
            {
                Debug.Log(Vector3.Distance(transform.position, navPoints[i - 1].position) < 0.1f);
                if (Vector3.Distance(transform.position, navPoints[i-1].position) < 0.1f)
                {

                    if(i>1) transform.LookAt(transform.position - (navPoints[i-2].position- transform.position));
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
