using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    // Start is called before the first frame update
    private List<Listener> listeners = new List<Listener>();

    public void TriggerEvent()
    {
        foreach (var listener in listeners)
        {
            Debug.Log("Event Triggered");
            listener.OnEventTrigger();
        }
    }

    public void AddListener(Listener listener)
    {
        Debug.Log("Listener added");
        listeners.Add(listener);
    }

    public void RemoveListener(Listener listener)
    {
        Debug.Log("Listener removed");
        listeners.Remove(listener);
    }
}
