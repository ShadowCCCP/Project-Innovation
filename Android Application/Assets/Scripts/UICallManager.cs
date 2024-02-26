using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class UICallManager : MonoBehaviour
{
    [SerializeField]
    Transform contentCalls;

    [SerializeField]
    GameObject callPrefab;

    [SerializeField]
    Transform contentMessages;

    [SerializeField]
    GameObject messagePrefab;

    List<GameObject> callList = new List<GameObject>();

    List<GameObject> messageList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewCall(Contact contact) //adds a new call in the call history
    {
        GameObject newCall = Instantiate(callPrefab, contentCalls);
        var contactName = newCall.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        var contactDetails = newCall.transform.Find("Details").GetComponent<TextMeshProUGUI>();
        var contactTime = newCall.transform.Find("Time").GetComponent<TextMeshProUGUI>();
        var contactImage = newCall.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

        contactName.text = contact.ContactName;
        contactDetails.text = contact.Details;
        contactImage.sprite = contact.Icon;
        contactTime.text = GameManager.Instance.GetCurrentTimeFormatted();

        callList.Add(newCall);

    }

    public void AddNewMessage(Contact contact, string message) //adds a new message in the message history
    {
        GameObject newMessage = Instantiate(messagePrefab, contentMessages);

        var contactName = newMessage.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        var contactDetails = newMessage.transform.Find("Details").GetComponent<TextMeshProUGUI>();
        var contactImage = newMessage.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
        var messageContent = newMessage.transform.Find("Message").GetComponent<TextMeshProUGUI>();
        var contactTime = newMessage.transform.Find("Time").GetComponent<TextMeshProUGUI>();

        contactName.text = contact.ContactName;
        contactDetails.text = contact.Details;
        contactImage.sprite = contact.Icon;
        messageContent.text = message;
        contactTime.text = GameManager.Instance.GetCurrentTimeFormatted();
    }
}
