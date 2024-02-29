using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPickerOrdered : MonoBehaviour, ICallManager
{
    Contact[] contacts;
    int currentCall;

    void Start()
    {
        currentCall = 0;

        EventBus<CallEvent>.OnEvent += PickCall;
    }

    void OnDestroy()
    {
        EventBus<CallEvent>.OnEvent -= PickCall;
    }

    public void PickCall(CallEvent callEvent)
    {
        Debug.Log("pick");
        contacts = GameManager.Instance.GetContacts();

        if (contacts == null || contacts.Length == 0) Debug.Log("MessagePicker: GameManager doesn't hold any contacts...");

        ShowCallReceived(contacts[currentCall]);
    }

    void ShowCallReceived(Contact caller)
    {
        if (!GameManager.Instance.IsActive())
        {
            GameManager.Instance.ToggleUIState();
            UIManager.Instance.ShowNewCall(caller);
            GameManager.Instance.GetUICallManager().AddNewCall(caller);
        }
        else
        {
            Debug.Log("CallPickerOrdered: Too many things happening at once...");
        }
    }

    public void ShowCallEnded()
    {
        StartCoroutine(UIManager.Instance.CallEnded());
        IncreaseCallIndex();
    }

    void IncreaseCallIndex()
    {
        currentCall++;

        if(currentCall >= contacts.Length)
        {
            currentCall = 0;
        }
    }
}
