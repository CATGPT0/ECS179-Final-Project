using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour
{
    public Action<int> OnPlayerHealthChanged;
    public UnityEvent OnPlayerAttack;
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
