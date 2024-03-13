using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.KennethDevelops.Events;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnPlayerLevelUp : BaseEvent<OnPlayerLevelUp>
{
    public int level;
    public int health;
    public int attack;
    public int defense;
    public float speed;
}

public class OnPlayerGetXP : BaseEvent<OnPlayerGetXP>
{
    public int xp;
}

public class PlayerEvent : MonoBehaviour
{
    public Action<int> OnPlayerHealthChanged;
    public UnityEvent OnPlayerAttack;
    public UnityEvent OnPlayerDeathEnter;
    public UnityEvent OnPlayerDeathExit;
    public UnityEvent OnPlayerRespawn;
    //public Action<int> OnPlayerGetXP;
    public Action OnPlayerGetXPSound;
    //public Action OnPlayerLevelUpSound;
    public Action OnGetHitSound;
    public Action OnPlayerGitHit;
    //public Action<int, float, int, int, int> OnPlayerLevelUp;
    public OnPlayerLevelUp onPlayerLevelUp;
    public OnPlayerGetXP onPlayerGetXP;

    void Awake()
    {
        onPlayerLevelUp = new OnPlayerLevelUp();
        onPlayerGetXP = new OnPlayerGetXP();
    }
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
