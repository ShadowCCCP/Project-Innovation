using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAndMessageManager : MonoBehaviour
{
    [SerializeField]
    List<Contact> contacts = new List<Contact>();

    [SerializeField]
    List<string> messages = new List<string>();

    bool messageOrCallOngoing;
    // Start is called before the first frame update

    [SerializeField]
    UICallManager uICallManager;
    void Start()
    {

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
