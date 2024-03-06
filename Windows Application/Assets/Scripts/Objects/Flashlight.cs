using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float radius = 1f;
    [SerializeField] float raycastDistance = 10f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Color color;
    StickyNoteManager noteManager;
    [SerializeField]
    GameObject spotLight;
    [SerializeField]
    bool lightON = false;
    string targetTag = "Monster";
    string infoTag = "Note";
    void Awake()
    {
        EventBus<FlashlightOnOffEvent>.OnEvent += turnOnOffFlashlight;
    }
    void OnDestroy()
    {
        EventBus<FlashlightOnOffEvent>.OnEvent -= turnOnOffFlashlight;
    }

    private void Start()
    {
        spotLight.SetActive(lightON);

        noteManager = GameManager.Instance.GetNoteManager();
    }



    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = -transform.up;

        RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction, raycastDistance, layerMask);

        //Debug.DrawRay(origin, direction * raycastDistance, Color.green);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag(targetTag))
            {
                Debug.Log("Monster detected!!! AAAA");
                hit.collider.gameObject.GetComponent<EnemyBehaviour>().OnSpotted();
            }
            if (hit.collider.CompareTag(infoTag))
            {
                Debug.Log("sticky note");
                noteManager.OnFlashLightHover(hit.collider.gameObject.GetComponent<StickyNote>());
            }
        }
    }

    public void turnOnOffFlashlight(FlashlightOnOffEvent flashlightOnOffEvent)
    {
        if (!lightON)
        {
            spotLight.SetActive(true);
            lightON = true;
        }
        else
        {
            spotLight.SetActive(false);
            lightON = false;
        }
    }
}
