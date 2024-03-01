using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EventManager : MonoBehaviour
{
    public Dictionary<GameObject, List<Action>> deathEvent = new Dictionary<GameObject, List<Action>>();
    public void DestroyObject(GameObject obj)
    {
        //deathEvent[obj].Invoke();
        Destroy(obj);
    }
}
}
