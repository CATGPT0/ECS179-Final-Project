using System.Collections;
using System.Collections.Generic;
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
        private set { direction = value; }
    }

    private Vector3 latestMoveDirection;
    public Vector3 LatestMoveDirection
    {
        get { return latestMoveDirection; }
        private set { latestMoveDirection = value; }
    }
    public PlayerProperties(int level, EntityType.Type type) : base(level, type)
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
    }
}
public class Player : Entity
{
    public new PlayerProperties properties = new PlayerProperties(1, EntityType.Type.Player);
    void Start()
    {
        Debug.Log("Player Start");
        Debug.Log("Player Level: " + properties.Level);
        Debug.Log("Player Health: " + properties.Health);
        Debug.Log("Player AttackPower: " + properties.AttackPower);
        Debug.Log("Player Speed: " + properties.Speed);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
