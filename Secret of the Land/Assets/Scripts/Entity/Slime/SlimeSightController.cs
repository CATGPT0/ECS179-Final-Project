using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSightController : MonoBehaviour
{
    [SerializeField]
    private SlimeFSM fsm;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fsm.properties.SeePlayer = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fsm.properties.SeePlayer = false;
        }
    }
}
