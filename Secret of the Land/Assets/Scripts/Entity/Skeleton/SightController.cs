using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public GameEvent onPlayerSighted;
    public GameEvent onPlayerLost;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerSighted.TriggerEvent();
            Debug.Log("Player Sighted");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerLost.TriggerEvent();
            Debug.Log("Player Lost");
        }
    }
}
