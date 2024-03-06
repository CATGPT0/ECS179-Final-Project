using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResumeEvent
{
    public static Action resumeEvent;

    public static void Register(Action onEvent)
    {
        resumeEvent += onEvent;
    }

    public static void Unregister(Action onEvent)
    {
        resumeEvent -= onEvent;
    }

    public static void Trigger()
    {
        resumeEvent?.Invoke();
    }
}
