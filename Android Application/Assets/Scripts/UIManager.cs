using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject flashlightIconOverlay;
    [SerializeField]
    GameObject callReceievedPrefab;
    [SerializeField]
    GameObject callOngoingPrefab;
    [SerializeField]
    GameObject newMessagePrefab;

    [SerializeField]
    CallAndMessageManager callAndMessageManager;

    [SerializeField]
    Slider flashCooldown;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    TextMeshProUGUI clock;
    // Start is called before the first frame update
    void Awake()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFlashlightOverlay(bool show)
    {
        flashlightIconOverlay.SetActive(show);
    }
    GameObject newCall;
    GameObject newCallOngoing;
    public void ShowNewCall(Contact contact)
    {
        newCallOngoing = Instantiate(callOngoingPrefab,transform);
        var contactNameAndDetailsOngoingCall = newCallOngoing.transform.Find("NameAndDetails").GetComponent<TextMeshProUGUI>();
        var contactImageOngoingCall = newCallOngoing.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

        contactNameAndDetailsOngoingCall.text = contact.ContactName + " - "+ contact.Details;
        contactImageOngoingCall.sprite = contact.Icon;

        newCall = Instantiate(callReceievedPrefab, transform);
        var contactNameNewCall = newCall.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        var contactDetailsNewCall = newCall.transform.Find("Details").GetComponent<TextMeshProUGUI>();
        var contactImageNewCall = newCall.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

        contactNameNewCall.text = contact.ContactName;
        contactDetailsNewCall.text = contact.Details;
        contactImageNewCall.sprite = contact.Icon;

        newCall.transform.Find("Answer").GetComponent<Button>().onClick.AddListener(AnswerCall);
        
    }

    public void AnswerCall()
    {
        //callReceieved.SetActive(false);

        Destroy(newCall);
    }

    public IEnumerator CallEnded()
    {
        var callDetails = newCallOngoing.transform.Find("CallDetails").GetComponent<TextMeshProUGUI>();
        
        callDetails.text = "Call Ended";
        yield return new WaitForSeconds(1);

        Animator callEndAnim = newCallOngoing.GetComponent<Animator>();
        callEndAnim.SetTrigger("StartAnimation");
        yield return new WaitForSeconds(2);
        Destroy(newCallOngoing);
        callAndMessageManager.EmptyState();
    }

    GameObject newMessage;
    public IEnumerator NewMessage(Contact contact, string message)
    {
        newMessage = Instantiate(newMessagePrefab,  transform);

        var contactNameAndDetailsMessage = newMessage.transform.Find("NameAndDetails").GetComponent<TextMeshProUGUI>();
        var contactImageMessage = newMessage.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
        var messageContent = newMessage.transform.Find("Message").GetComponent<TextMeshProUGUI>();

        contactNameAndDetailsMessage.text = contact.ContactName + " - " + contact.Details;
        contactImageMessage.sprite = contact.Icon;
        messageContent.text = message;


        Animator newMessageAnim = newMessage.GetComponent<Animator>();
        newMessageAnim.SetTrigger("StartAnimation");
        yield return new WaitForSeconds(7);
        Destroy(newMessage);
        callAndMessageManager.EmptyState();
    }


    public void UpdateFlashlightSlider(float value)
    {
        flashCooldown.value = value;
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void UpdateClock(string hourString, string minuteString)
    {
        
        clock.SetText(hourString + ":" + minuteString); 
        
    }
  
}
