using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePauseEvent
{
    public static Action pauseEvent;

    public static void Register(Action onEvent)
    {
        pauseEvent += onEvent;
    }

    public static void Unregister(Action onEvent)
    {
        pauseEvent -= onEvent;
    }

    public static void Trigger()
    {
        pauseEvent?.Invoke();
    }
}
