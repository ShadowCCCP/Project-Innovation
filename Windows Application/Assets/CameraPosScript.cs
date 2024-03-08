using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosScript : MonoBehaviour
{
    [SerializeField] Transform startPosition;

    void Start()
    {
        EventBus<GameStartEvent>.OnEvent += SetPosition;
    }

    void OnDestroy()
    {
        EventBus<GameStartEvent>.OnEvent -= SetPosition;
    }

    void SetPosition(GameStartEvent gameStartEvent)
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }
}
