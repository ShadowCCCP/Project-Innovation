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
    }

  
}
