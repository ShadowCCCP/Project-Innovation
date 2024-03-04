using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameOverType { DeadResident, DeadthByMonster, Won }

    public static GameManager Instance;
    UIManager uIManager;
    Animator anim;

    StickyNoteManager stickyNoteManager;

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

        stickyNoteManager = GetComponent<StickyNoteManager>();
    }

    void OnDestroy()
    {
        EventBus<GameRestartedEvent>.OnEvent -= RestartGame;
        EventBus<GameOverEvent>.OnEvent -= GameOver;
    }
    void Start()
    {
        uIManager = GetComponentInChildren<UIManager>();
        anim = GetComponent<Animator>();
    }


    public void RestartGame(GameRestartedEvent gameRestartedEvent)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        anim.SetTrigger("Fade");
    }

    void GameOver(GameOverEvent gameOverEvent)
    {
        uIManager.ShowGameOverUI(gameOverEvent.gameOverType);
    }

    public StickyNoteManager GetNoteManager()
    {
        return stickyNoteManager;
    }
}
