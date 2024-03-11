using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class GameOverButton : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Awake()
    {
        
    }

    public void ContinueGame()
    {
        player = GameObject.Find("PlayerManager");
        if (player == null)
        {
            Debug.LogError("Player is null");
        
        }
        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(true);
        }
        player.GetComponentInChildren<PlayerController>().gameObject.SetActive(true);
        if (player.GetComponentInChildren<PlayerController>().gameObject == null)
        {
            Debug.LogError("PlayerEvent is null");
        }
        player.GetComponentInChildren<PlayerEvent>().OnPlayerRespawn.Invoke();
        Debug.Log("Continue Game");
    }
}
