using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties
{
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private int armor;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    private int magicResist;
    public int MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    private AttackType attackType;
    public AttackType AttackType
    {
        get { return attackType; }
        private set { attackType = value; }
    }
}
