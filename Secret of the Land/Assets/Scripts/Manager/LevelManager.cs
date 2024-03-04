using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerEvent playerEvent;
    [SerializeField]
    private XPComponent xp;
    public XPComponent XP
    {
        get { return xp; }
        set { xp = value; }
    }
    [SerializeField]
    private LevelComponent level;
    public LevelComponent Level
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
    private Dictionary<int, int> levelThresholds = new Dictionary<int, int>()
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
        xp = GetComponentInChildren<XPComponent>();
        level = GetComponentInChildren<LevelComponent>();
        entity = FindFirstObjectByType<Entity>();
        playerEvent = FindFirstObjectByType<PlayerEvent>();
    }

    public void ReceiveXP(int amount)
    {
        xp.IncreaseBy(amount);
        while (xp.Get() >= levelThresholds[level.Get()])
        {
            xp.SetTo(xp.Get() - levelThresholds[level.Get()]);
            level.IncreaseBy(1);
            entity.AttackPower.IncreaseBy(attackPowerPerLevel);
            entity.Health.IncreaseBy(healthPerLevel);
            entity.Armor.IncreaseBy(armorPerLevel);
            entity.MagicResist.IncreaseBy(magicResistPerLevel);
            Debug.Log("Level Become: " + level.Get());
            Debug.Log("Attack Power Become: " + entity.AttackPower.Get());
            Debug.Log("Health Become: " + entity.Health.Get());
            Debug.Log("Armor Become: " + entity.Armor.Get());
            Debug.Log("Magic Resist Become: " + entity.MagicResist.Get());
        }  
        Debug.Log("XP Become: " + xp.Get());
        playerEvent.LevelUpEvent?.TriggerEvent();
    }
}
