using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float radius = 1f;
    [SerializeField] float raycastDistance = 10f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Color color;
    StickyNoteManager noteManager;

    string targetTag = "Monster";
    string infoTag = "Note";

    private void Start()
    {
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
                //hit.collider.gameObject.GetComponent<StickyNote>().OnFlashLightHover();
                noteManager.OnFlashLightHover(hit.collider.gameObject.GetComponent<StickyNote>());
            }
        }
    }
}
