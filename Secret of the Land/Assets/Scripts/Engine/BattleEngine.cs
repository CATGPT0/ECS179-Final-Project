using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType
{
    Physical,
    Magical
}

    
public class BattleEngine : MonoBehaviour
{
    private HealthBarController healthBarController;

    void Awake()
    {
        healthBarController = FindFirstObjectByType<HealthBarController>();
    }

    public void DealDamage(ref Properties attacker, ref Properties victim, AttackType type)
    {
        int attackPower = attacker.AttackPower;
        int damage = 0;
        if (victim == null)
        {
            Debug.Log("Victim is null");
        }
        
        if (type == AttackType.Physical)
        {
            damage = attackPower - victim.Armor;
        }
        else if (type == AttackType.Magical)
        {
            damage = attackPower - victim.MagicResist;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        victim.Health -= damage;

        if (victim.ThisType == EntityType.Type.Player)
        {
            healthBarController.SetHealth(victim.Health);
        }
    }
}