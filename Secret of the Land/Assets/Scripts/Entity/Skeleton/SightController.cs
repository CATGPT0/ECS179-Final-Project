using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public GameEvent onPlayerSighted;
    public GameEvent onPlayerLost;
    [SerializeField]
    private SkeletonFSM fsm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fsm.properties.SeePlayer = true;
            //onPlayerSighted.TriggerEvent();
            //Debug.Log("Player Sighted");
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fsm.properties.SeePlayer = false;
            //onPlayerLost.TriggerEvent();
            //Debug.Log("Player Lost");
        }
    }
}
