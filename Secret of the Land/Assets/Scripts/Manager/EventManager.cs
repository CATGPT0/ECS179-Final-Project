using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EventManager : MonoBehaviour
{
    private Dictionary<GameObject, Action> deathEvent = new Dictionary<GameObject, Action>();
    public void DestroyObject(GameObject obj)
    {
        deathEvent[obj].Invoke();
        Destroy(obj);
    }
}
}
