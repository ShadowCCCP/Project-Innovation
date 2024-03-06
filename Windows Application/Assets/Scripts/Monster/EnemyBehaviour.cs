using FMODUnity;
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
    float killingSpeed = 20f;
    [SerializeField]
    List<Transform> navPoints = new List<Transform>();

    [SerializeField] EventReference soundReference;
    [SerializeField] string parameterName;
    SoundSystem soundSystem;

    int currentParamValue;
    int i;

    [SerializeField]
    bool startMovementOnStart = false;

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
        anim = GetComponentInChildren<Animator>();
        transform.LookAt(navPoints[i].position);

        if (startMovementOnStart) 
        { 

            currentSpeed = speed;
        }
        anim.SetFloat("Speed", currentSpeed);
        // Get or create soundSystem component and set soundReference inside...
        soundSystem = GetComponent<SoundSystem>();
        if (soundSystem == null)
        {
            soundSystem = gameObject.AddComponent<SoundSystem>();
        }
        soundSystem.SetSoundValues(soundReference, false);
        soundSystem.PlaySound(); 
    }

    void Update()
    {
        currentParamValue = (int)Mathf.Clamp(currentSpeed, -3, 4);
        soundSystem.SetParameterLocal(parameterName, currentParamValue);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        if (currentSpeed > 0)
        {
            moveForward();
        }
        else if (currentSpeed <0)
        {
            moveBackwards();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnSpotted();
        }
    }

    void moveForward()
    {
        if (i < navPoints.Count-1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(navPoints[i].position - transform.position, Vector3.up), rotiatonSpeed * Time.deltaTime *currentSpeed);
            if (Vector3.Distance(transform.position, navPoints[i].position) < 0.1f)
            {
                i++;

            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(navPoints[i].position - transform.position, Vector3.up), rotiatonSpeed * Time.deltaTime * currentSpeed);
           
            StartCoroutine(killingCoroutine());
            if (Vector3.Distance(transform.position, navPoints[i].position) < 1f)
            {
                currentSpeed = 0;
                //anim.SetFloat("Speed", currentSpeed);
            }
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
        if (monsterEvent.monsterID == monsterID && currentSpeed ==0)
        {
            currentSpeed = speed;
        }
    }

    IEnumerator killingCoroutine()
    {
        //anim.SetTrigger("AttackAnimation");
       // Debug.Log("de");

        currentSpeed = killingSpeed; 
        anim.SetFloat("Speed", currentSpeed);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("AttackAnimation");
        yield return new WaitForSeconds(5);

        EventBus<GameOverEvent>.Publish(new GameOverEvent(GameManager.GameOverType.DeadthByMonster));
    }
}
