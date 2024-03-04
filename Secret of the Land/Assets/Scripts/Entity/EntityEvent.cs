using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEvent : MonoBehaviour
{
    [SerializeField] 
    protected GameEvent deathEvent;
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void OnDestroy()
    {
        deathEvent?.TriggerEvent();
    }
}
