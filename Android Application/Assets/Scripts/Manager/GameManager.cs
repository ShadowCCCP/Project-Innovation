using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    IMessageManager messageManager;
    ICallManager callManager;

    [SerializeField] UICallManager uiCallManager;
    [SerializeField] Contact[] contacts;
    [SerializeField] [Multiline(3)] string[] messages;

    TimeManager timeManager;
    bool messageOrCallOngoing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        EventBus<GameRestartEvent>.OnEvent += RestartGame;
    }

    private void OnDestroy()
    {

        EventBus<GameRestartEvent>.OnEvent += RestartGame;
    }

    void Start()
    {
        messageManager = GetComponent<IMessageManager>();
        callManager = GetComponent<ICallManager>();
        timeManager = GetComponentInChildren<TimeManager>();

        if (messageManager == null) Debug.Log("GameManager: MessageManager missing...");
        else if (callManager == null) Debug.Log("GameManager: CallManager missing...");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            callManager.PickCall(new CallEvent());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            callManager.ShowCallEnded();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            messageManager.PickMessage(new MessageEvent());
        }
    }

    public void EmptyState()
    {
        messageOrCallOngoing = false;
        Debug.Log("state emptied");
    }

    public string[] GetMessages()
    {
        return messages;
    }

    public Contact[] GetContacts()
    {
        return contacts;
    }

    public UICallManager GetUICallManager()
    {
        return uiCallManager;
    }

    public bool IsActive()
    {
        return messageOrCallOngoing;
    }

    public void ToggleUIState()
    {
        messageOrCallOngoing = !messageOrCallOngoing;
    }

    public TimeManager GetTimeManager()
    {
        return timeManager;
    }

    private void RestartGame(GameRestartEvent gameRestartEvent)
    {
        UDPSender.SendBroadcast("Restart");
    }

    private void GameOver(GameOverEvent gameOverEvent)
    {

    }
}
