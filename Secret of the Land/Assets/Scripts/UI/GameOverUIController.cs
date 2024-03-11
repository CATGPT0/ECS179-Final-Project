using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    private PlayerEvent playerEvent;
    [SerializeField]
    private GameObject gameOverUI;

    void Awake()
    {
        playerEvent = FindFirstObjectByType<PlayerEvent>();
    }

    void Start()
    {
        playerEvent.OnPlayerDeathExit.AddListener(SetGameOverUIActive);
        playerEvent.OnPlayerRespawn.AddListener(SetGameOverUIDeactive);
    }

    void SetGameOverUIActive()
    {
        gameOverUI.SetActive(true);
    }

    void SetGameOverUIDeactive()
    {
        gameOverUI.SetActive(false);
    }
}

