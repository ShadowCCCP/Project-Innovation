using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameOverType { DeadResident, DeadthByMonster, Won }

    public static GameManager Instance;

    [SerializeField] ContactInfo[] contactInfo;
    [SerializeField] EventReference ambientSound;

    Animator anim;

    UIManager uIManager;
    StickyNoteManager stickyNoteManager;

    bool residentDead;

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

        EventBus<GameRestartedEvent>.OnEvent += RestartGame;
        EventBus<GameOverEvent>.OnEvent += GameOver;
        EventBus<GameStartEvent>.OnEvent += StartGame;

        stickyNoteManager = GetComponent<StickyNoteManager>();
    }

    void OnDestroy()
    {
        EventBus<GameRestartedEvent>.OnEvent -= RestartGame;
        EventBus<GameOverEvent>.OnEvent -= GameOver;
        EventBus<GameStartEvent>.OnEvent -= StartGame;
    }

    void Start()
    {
        uIManager = GetComponentInChildren<UIManager>();
        anim = GetComponent<Animator>();
    }


    public void RestartGame(GameRestartedEvent gameRestartedEvent)
    {
        anim.SetTrigger("FadeOut");
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("FadeOut");
    }

    void GameOver(GameOverEvent gameOverEvent)
    {
        uIManager.ShowGameOverUI(gameOverEvent.gameOverType);
    }

    void StartGame(GameStartEvent gameStartEvent)
    {
        uIManager.ToggleMainMenu();
        anim.SetTrigger("FadeIn");
    }

    public bool GetResidentDead()
    {
        return residentDead;
    }

    public void SetResidentDead(bool state)
    {
        residentDead = state;
    }

    public StickyNoteManager GetNoteManager()
    {
        return stickyNoteManager;
    }
}
