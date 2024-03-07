using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Events;

public class Listener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    public UnityEvent onEventTrigger;

    void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTrigger()
    {
        onEventTrigger.Invoke();
    }
}
