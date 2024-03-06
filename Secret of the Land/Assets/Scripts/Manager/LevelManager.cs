using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerEvent playerEvent;
    private LevelUpController levelUpController;
    private GetXPTextController getXPTextController;
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
    [SerializeField]
    private float speedPerLevel;
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
        entity = FindFirstObjectByType<Player>();
        playerEvent = FindFirstObjectByType<PlayerEvent>();
        levelUpController = FindFirstObjectByType<LevelUpController>();
        getXPTextController = FindFirstObjectByType<GetXPTextController>();
    }

    public void ReceiveXP(int amount)
    {
        xp += amount;
        getXPTextController.XPUIShowUp(amount);
        bool levelUp = false;
        while (xp >= levelThresholds[level])
        {
            xp = xp - levelThresholds[level];
            ++level;
            entity.AttackPower += attackPowerPerLevel;
            entity.MaxHealth += healthPerLevel;
            entity.Armor += armorPerLevel;
            entity.MagicResist += magicResistPerLevel;
            entity.Speed += speedPerLevel;

            levelUp = true;
        }
        if (levelUp)
        {
            levelUpController.LevelUIShowUp(healthPerLevel, speedPerLevel, attackPowerPerLevel, armorPerLevel, level);
            entity.Health = entity.MaxHealth;
        }
        Debug.Log("XP Become: " + xp);
        playerEvent.LevelUpEvent?.TriggerEvent();
    }
}
