using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeEvent : MonoBehaviour
{
    public UnityEvent onMonsterDeath;
    public Action onMonsterHealthChanged;
}
