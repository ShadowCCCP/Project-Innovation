using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAndMessageManager : MonoBehaviour
{
    [SerializeField]
    List<Contact> contacts = new List<Contact>();
    // Start is called before the first frame update
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
            //OnCallEnded();
            StartCoroutine( UIManager.Instance.CallEnded());
        }
    }

    public void OnCallReceived(Contact caller)
    {
        UIManager.Instance.ShowNewCall(caller);
    }

        

    public void ActivePushToTalk() { }

    public void EndPushToTalk() { }

    public void OnMessageReceived()
    {
        //do animation
    }
}
