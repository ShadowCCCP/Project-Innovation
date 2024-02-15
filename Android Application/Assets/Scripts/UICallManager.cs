using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICallManager : MonoBehaviour
{
    [SerializeField]
    Transform content;

    [SerializeField]
    GameObject callPrefab;

    List<GameObject> callList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewCall(Contact contact)
    {
        GameObject newCall = Instantiate(callPrefab, content);
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
}
