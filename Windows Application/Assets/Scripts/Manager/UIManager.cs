using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
