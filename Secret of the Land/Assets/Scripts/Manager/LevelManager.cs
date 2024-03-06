using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerEvent playerEvent;
    [SerializeField]
    private int xp;
    public int XP
    {
        get { return xp; }
        set { xp = value; }
    }
    [SerializeField]
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    [SerializeField]
    private int healthPerLevel;
    [SerializeField]
    private int attackPowerPerLevel;
    [SerializeField]
    private int armorPerLevel;
    [SerializeField]
    private int magicResistPerLevel;

    private Entity entity;
    public Dictionary<int, int> levelThresholds = new Dictionary<int, int>()
    {
        {1, 100},
        {2, 250},
        {3, 500},
        {4, 800},
        {5, 1200},
        {6, 1800},
        {7, 2500},
        {8, 3500},
        {9, 5000},
        {10, 7000}
    };

    void Awake()
    {
        entity = FindFirstObjectByType<Entity>();
        playerEvent = FindFirstObjectByType<PlayerEvent>();
    }

    public void ReceiveXP(int amount)
    {
        xp += amount;
        while (xp >= levelThresholds[level])
        {
            xp = xp - levelThresholds[level];
            ++level;
            entity.AttackPower += attackPowerPerLevel;
            entity.Health += healthPerLevel;
            entity.Armor += armorPerLevel;
            entity.MagicResist += magicResistPerLevel;
            Debug.Log("Level Become: " + level);
            Debug.Log("Attack Power Become: " + entity.AttackPower);
            Debug.Log("Health Become: " + entity.Health);
            Debug.Log("Armor Become: " + entity.Armor);
            Debug.Log("Magic Resist Become: " + entity.MagicResist);
        }  
        Debug.Log("XP Become: " + xp);
        playerEvent.LevelUpEvent?.TriggerEvent();
    }
}
