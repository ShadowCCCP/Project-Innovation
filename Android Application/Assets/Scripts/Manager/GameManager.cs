using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Contact[] contacts;
    [SerializeField] [Multiline(3)] string[] messages;

    bool messageOrCallOngoing;
    // Start is called before the first frame update

    [SerializeField]
    UICallManager uICallManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnCallReceived(contacts[0]);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnCallEnded();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            OnNewMessage(contacts[0], messages[0]);
        }
    }

    public void OnCallReceived(Contact caller)
    {
        if (!messageOrCallOngoing)
        {
            messageOrCallOngoing = true;
            UIManager.Instance.ShowNewCall(caller);
            uICallManager.AddNewCall(caller);
        }
        else
        {
            Debug.Log("too many things happening at once");
        }
    }

    public void OnCallEnded()
    {
        StartCoroutine(UIManager.Instance.CallEnded());
    }    

    public void OnNewMessage(Contact contact, string message)
    {
        if (!messageOrCallOngoing)
        {
            messageOrCallOngoing = true;
            uICallManager.AddNewMessage(contact, message);
            StartCoroutine(UIManager.Instance.NewMessage(contact, message));
        }
        else
        {
            Debug.Log("too many things happening at once");
        }
    }

    public void ActivePushToTalk() { }

    public void EndPushToTalk() { }

    public void OnMessageReceived()
    {
        //do animation
    }

    public void EmptyState()
    {
        messageOrCallOngoing = false;
        Debug.Log("state emptied");
    }
}
