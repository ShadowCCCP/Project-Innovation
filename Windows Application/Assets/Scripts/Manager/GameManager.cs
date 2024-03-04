using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    UIManager uIManager;
    Animator anim;

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

        EventBus<GameRestartedEvent>.OnEvent += TriggerFade;
        EventBus<GameOverEvent>.OnEvent += GameOver;
    }

    void OnDestroy()
    {
        EventBus<GameRestartedEvent>.OnEvent -= TriggerFade;
        EventBus<GameOverEvent>.OnEvent -= GameOver;
    }
    void Start()
    {
        uIManager = GetComponentInChildren<UIManager>();
        anim = GetComponent<Animator>();
    }

    void TriggerFade(GameRestartedEvent gameRestartedEvent)
    {
        anim.SetTrigger("Fade");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        anim.SetTrigger("Fade");
    }

    void GameOver(GameOverEvent gameOverEvent)
    {
        //show ui
        uIManager.ShowGameOverUI();
    }
}
