using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    int monsterID = 0;
    [SerializeField]
    float speed = 5;

    float currentSpeed = 0; 
    [SerializeField]
    float rotiatonSpeed = 2f;
    [SerializeField]
    float backwardsSpeed = -5f;
    [SerializeField]
    List<Transform> navPoints = new List<Transform>();

    Animator anim;
    void Awake()
    {
        EventBus<MonsterEvent>.OnEvent += startMovement;
    }
    void OnDestroy()
    {
        EventBus<MonsterEvent>.OnEvent -= startMovement;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = navPoints[i].position;
        anim = GetComponent<Animator>();
        transform.LookAt(navPoints[i].position);
        anim.SetFloat("Speed", currentSpeed);
    }

    int i =0;
    void Update()
    {
        anim.SetFloat("Speed", currentSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        if (currentSpeed > 0)
        {
            moveForward();
        }
        else if (currentSpeed <0)
        {
            moveBackwards();
        }
        
    }

    void moveForward()
    {
        if (i < navPoints.Count)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(navPoints[i].position - transform.position, Vector3.up), rotiatonSpeed * Time.deltaTime *currentSpeed);
            if (Vector3.Distance(transform.position, navPoints[i].position) < 0.1f)
            {
                i++;

            }
        }
        else
        {
            //gameover
            currentSpeed = 0; 
            anim.SetFloat("Speed", currentSpeed);
        }
    }

    void moveBackwards()
    {
        if (i > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-(navPoints[i - 1].position - transform.position), Vector3.up), rotiatonSpeed * Time.deltaTime * -currentSpeed);
            if (Vector3.Distance(transform.position, navPoints[i - 1].position) < 0.1f)
            {
                i--;
            }
        }
        else
        {
            currentSpeed = 0;
            anim.SetFloat("Speed", currentSpeed);
        }
    }

    public void OnSpotted()
    {
        currentSpeed = backwardsSpeed;
        anim.SetFloat("Speed", currentSpeed);
    }

    void startMovement(MonsterEvent monsterEvent)
    {
        if (monsterEvent.monsterID == monsterID)
        {
            currentSpeed = speed;
        }
    }
}
