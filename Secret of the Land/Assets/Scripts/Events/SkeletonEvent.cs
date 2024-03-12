using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using UnityEngine.Events;

public class SkeletonEvent : MonoBehaviour
{
    public UnityEvent onMonsterDeath;
    public Action onMonsterHealthChanged;

}
