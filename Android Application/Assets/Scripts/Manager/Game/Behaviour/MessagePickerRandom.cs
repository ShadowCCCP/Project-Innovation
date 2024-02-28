using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class MessagePickerRandom : MonoBehaviour, IMessageManager
{
    Contact[] contacts;
    string[] messages;

    public void PickMessage()
    {
        Contact[] contacts = GameManager.Instance.GetContacts();
        string[] messages = GameManager.Instance.GetMessages();

        if (contacts == null || contacts.Length == 0) Debug.Log("MessagePicker: GameManager doesn't hold any contacts...");
        else if (messages == null || messages.Length == 0) Debug.Log("MessagePicker: GameManager doesn't hold any messages...");

        int messageRand = Random.Range(0, messages.Length);
        int contactRand = Random.Range(0, contacts.Length);

        ShowMessage(contacts[contactRand], messages[messageRand]);
    }

    void ShowMessage(Contact contact, string message)
    {
        if (!GameManager.Instance.IsActive())
        {
            GameManager.Instance.ToggleUIState();
            GameManager.Instance.GetUICallManager().AddNewMessage(contact, message);
            StartCoroutine(UIManager.Instance.NewMessage(contact, message));
        }
        else
        {
            Debug.Log("MessagePicker: Too many things happening at once...");
        }
    }
}
