using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour
{
    public Action<int> OnPlayerHealthChanged;
    public UnityEvent OnPlayerAttack;
    public UnityEvent OnPlayerDeathEnter;
    public UnityEvent OnPlayerDeathExit;
    public UnityEvent OnPlayerRespawn;
    public Action<int> OnPlayerGetXP;
    public Action OnPlayerGetXPSound;
    public Action OnPlayerLevelUpSound;
    public Action OnGetHitSound;
    public Action OnPlayerGitHit;
    public Action<int, float, int, int, int> OnPlayerLevelUp;

    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
