using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    UIManager uIManager;
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
    }

    void OnDestroy()
    {
        EventBus<GameRestartedEvent>.OnEvent -= RestartGame;
        EventBus<GameOverEvent>.OnEvent -= GameOver;
    }
    void Start()
    {
        uIManager = GetComponentInChildren<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartGame(GameRestartedEvent gameRestartedEvent)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    void GameOver(GameOverEvent gameOverEvent)
    {
        //show ui
        uIManager.ShowGameOverUI();

    }
}
