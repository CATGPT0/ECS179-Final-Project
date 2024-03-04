using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public class Entity : MonoBehaviour
{

    [SerializeField] 
    private GameEvent deathEvent;
    
    protected HealthComponent health;
    public HealthComponent Health
    {
        get { return health; }
        set { health = value; }
    }
    protected AttackPowerComponent attackPower;
    public AttackPowerComponent AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    protected ArmorComponent armor;
    public ArmorComponent Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    protected MagicResistComponent magicResist;
    public MagicResistComponent MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }

    protected void Init()
    {
        health = GetComponentInChildren<HealthComponent>();
        attackPower = GetComponentInChildren<AttackPowerComponent>();
        armor = GetComponentInChildren<ArmorComponent>();
        magicResist = GetComponentInChildren<MagicResistComponent>();
    }

    public void DeactivateSelf()
    {
        this.enabled = false;
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void OnDestroy()
    {
        deathEvent?.TriggerEvent();
    }
}

