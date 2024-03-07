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
            listener.OnEventTrigger();
        }
    }

    public void AddListener(Listener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(Listener listener)
    {
        listeners.Remove(listener);
    }
}
