using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;

public class MessagePickerRandom : MonoBehaviour, IMessageManager
{
    Contact[] contacts;
    string[] messages;

    void Start()
    {
        EventBus<MessageEvent>.OnEvent += PickMessage;
    }

    void OnDestroy()
    {
        EventBus<MessageEvent>.OnEvent -= PickMessage;
    }

    public void PickMessage(MessageEvent messageEvent)
    {
        contacts = GameManager.Instance.GetContacts();
        messages = GameManager.Instance.GetMessages();

        if (contacts == null || contacts.Length == 0) Debug.Log("MessagePicker: GameManager doesn't hold any contacts...");
        else if (messages == null || messages.Length == 0) Debug.Log("MessagePicker: GameManager doesn't hold any messages...");

        System.Random random = new System.Random();

        int contactRand = random.Next(0, contacts.Length);
        int messageRand = random.Next(0, messages.Length);

        ShowMessage(contacts[contactRand], messages[messageRand]);
    }

    void ShowMessage(Contact contact, string message)
    {
        if (!GameManager.Instance.IsActive())
        {
            //Debug.Log("1");
            GameManager.Instance.ToggleUIState();
            Debug.Log("2");
            UIManager.Instance.SetDataMessage(contact, message);
            //GameManager.Instance.GetUICallManager().SetData(contact, message);
            //GameManager.Instance.GetUICallManager().AddNewMessage(contact, message);
            //Debug.Log("3");
            //StartCoroutine(UIManager.Instance.NewMessage(contact, message));
        }
        else
        {
            Debug.Log("MessagePicker: Too many things happening at once...");
        }
    }

    
}
