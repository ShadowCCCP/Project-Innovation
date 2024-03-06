using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        EventBus<GameOverEvent>.OnEvent += GameOver;
        EventBus<GameWonEvent>.OnEvent += GameWon;
    }

    private void OnDestroy()
    {

        EventBus<GameRestartEvent>.OnEvent -= RestartGame;
        EventBus<GameOverEvent>.OnEvent -= GameOver;
        EventBus<GameWonEvent>.OnEvent -= GameWon;
    }

    void Start()
    {
        messageManager = GetComponent<IMessageManager>();
        callManager = GetComponent<ICallManager>();
        timeManager = GetComponentInChildren<TimeManager>();

        if (messageManager == null) Debug.Log("GameManager: MessageManager missing...");
        else if (callManager == null) Debug.Log("GameManager: CallManager missing...");

        InvokeRepeating("SendResidentsState", 5, 5);
    }

    void SendResidentsState()
    {
        UDPSender.SendBroadcast("Alive: " + AllResidentsAlive());
    }

    public bool AllResidentsAlive()
    {
        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].IsDead())
            {
                return false;
            }
        }

        return true;
    }

    public void StopCall()
    {
        callManager.ShowCallEnded();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UDPSender.SendBroadcast("Restart");
    }

    private void GameOver(GameOverEvent gameOverEvent)
    {
        UDPSender.SendBroadcast("Game Over");
    }

    private void GameWon(GameWonEvent gameWonEvent)
    {
        UDPSender.SendBroadcast("Game Won");
    }
}
