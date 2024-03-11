using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Schema.Builtin.Nodes;
using UnityEngine;

public class PlayerProperties : Properties
{
    private float velocityX;
    public float VelocityX
    {
        get { return velocityX; }
        set { velocityX = value; }
    }

    private float velocityY;
    public float VelocityY
    {
        get { return velocityY; }
        set { velocityY = value; }
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    private Vector3 latestMoveDirection;
    public Vector3 LatestMoveDirection
    {
        get { return latestMoveDirection; }
        set { latestMoveDirection = value; }
    }

    private Vector3 respawnPoint;
    public Vector3 RespawnPoint
    {
        get { return respawnPoint; }
        set { respawnPoint = value; }
    }

    private int xp;
    public int XP
    {
        get { return xp; }
        set 
        {   
            int temp = value - xp;
            xp = value; 
            playerEvent.OnPlayerGetXP?.Invoke(temp);
            playerEvent.OnPlayerGetXPSound?.Invoke();
        }
    }

    public override int Health
    {
        get { return health; }
        set
        {
            if (value < health)
            {
                playerEvent.OnGetHitSound?.Invoke();
                playerEvent.OnPlayerGitHit?.Invoke();
            }
            health = value;
            playerEvent.OnPlayerHealthChanged?.Invoke(health);
        }
    }

    public override int Level
    {
        get { return level; }
        set
        {
            level = value;
            playerEvent.OnPlayerLevelUp?.Invoke(Table.healthTable[thisType][level] - Table.healthTable[thisType][level - 1],
                                                Table.speedTable[thisType][level] - Table.speedTable[thisType][level - 1],
                                                Table.attackPowerTable[thisType][level] - Table.attackPowerTable[thisType][level - 1],
                                                Table.armorTable[thisType][level] - Table.armorTable[thisType][level - 1],
                                                level);
            playerEvent.OnPlayerLevelUpSound?.Invoke();
        }
    }

    private bool canMove;
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
   
    public PlayerEvent playerEvent;
    public PlayerProperties(int level, EntityType.Type type) : base()
    {
        thisType = type;
        this.level = level;
        maxHealth = Table.healthTable[type][level];
        health = maxHealth;
        attackPower = Table.attackPowerTable[type][level];
        speed = Table.speedTable[type][level];
        armor = Table.armorTable[type][level];
        magicResist = Table.magicResistTable[type][level];
        attackType = Table.attackTypeTable[type];
        canMove = true;
        xp = 0;
    }
}
public class Player : Entity
{
    public new PlayerProperties properties;
    private PlayerController playerController;
    void Awake()
    {
        properties = new PlayerProperties(1, EntityType.Type.Player);
        properties.playerEvent = FindFirstObjectByType<PlayerEvent>();
        properties.RespawnPoint = transform.position;
        playerController = GetComponentInParent<PlayerController>();
    }
    
    void Start()
    {
        playerController.PlayerEvent.OnPlayerRespawn.AddListener(SetHealthToFull);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test(int a)
    {
        Debug.Log("Test" + a);
    }

    public void ReceiveXP(int amount)
    {
        properties.XP += amount;
        Debug.Log("XP Become: " + properties.XP);
        bool levelUp = false;
        while (properties.XP >= Table.levelThresholds[properties.Level])
        {
            properties.XP = properties.XP - Table.levelThresholds[properties.Level];
            ++properties.Level;
            properties.AttackPower = Table.attackPowerTable[properties.ThisType][properties.Level];
            properties.MaxHealth = Table.healthTable[properties.ThisType][properties.Level];
            properties.Armor = Table.armorTable[properties.ThisType][properties.Level];
            properties.MagicResist = Table.magicResistTable[properties.ThisType][properties.Level];
            properties.Speed = Table.speedTable[properties.ThisType][properties.Level];
            levelUp = true;
        }
        if (levelUp)
        {
            properties.Health = properties.MaxHealth;
            playerController.healthBarController.SetMaxHealth(properties.MaxHealth);
        }
    }
    
    public void SetHealthToFull()
    {
        properties.Health = properties.MaxHealth;
    }
}
