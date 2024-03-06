using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameStartEvent
{
    private static Action thisEvent;

    public static void Register(Action onEvent)
    {
        thisEvent += onEvent;
    }

    public static void Unregister(Action onEvent)
    {
        thisEvent -= onEvent;
    }

    public static void Trigger()
    {
        thisEvent?.Invoke();
    }
}
