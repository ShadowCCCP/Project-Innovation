using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI gameOverCause;

    [SerializeField] GameObject mainMenu;

    public void ShowGameOverUI(GameManager.GameOverType gameOverType)
    {
        gameOverUI.SetActive(true);

        switch (gameOverType)
        {
            case GameManager.GameOverType.Won:
                gameOverCause.SetText("You Won!");
                break;
            case GameManager.GameOverType.DeadResident:
                gameOverCause.SetText("You killed a resident!");
                break;
            case GameManager.GameOverType.DeadthByMonster:
                gameOverCause.SetText("The monster tore down you insides!");
                break;
        }
    }

    public void ToggleMainMenu()
    {
        if (mainMenu.activeSelf) mainMenu.SetActive(false);
        else mainMenu.SetActive(true);
    }


}
