using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject button;
    ICallManager callPicker;

    [SerializeField] float waitTimeOne;
    [SerializeField] float waitTimeRest;
    [SerializeField] float waitTimeEnd;

    void Start()
    {
        callPicker = FindObjectOfType<CallPickerOrdered>();
        if (callPicker == null) Debug.Log("SpeechButtonHandler: No CallPickerOrdered script found...");

        EventBus<CallStageEvent>.OnEvent += CheckStage;
    }

    private void OnDestroy()
    {
        EventBus<CallStageEvent>.OnEvent -= CheckStage;
    }

    void CheckStage(CallStageEvent callStageEvent)
    {
        if (callStageEvent.stage == 1)
        {
            StartCoroutine(EnableButton(waitTimeOne));
        }
        else if (callStageEvent.stage <= 3)
        {
            StartCoroutine(EnableButton(waitTimeRest));
        }
        else StartCoroutine(EndCall(waitTimeEnd));
    }

    IEnumerator EnableButton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        button.SetActive(true);
    }

    IEnumerator EndCall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameManager.Instance.StopCall();
    }
}
