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

    public void DealDamage(Entity attacker, Entity victim, AttackType type)
    {
        int attackPower = attacker.AttackPower.Get();
        int damage = 0;
        
        if (type == AttackType.Physical)
        {
            damage = attackPower - victim.Armor.Get();
        }
        else if (type == AttackType.Magical)
        {
            damage = attackPower - victim.MagicResist.Get();
        }

        if (damage < 0)
        {
            damage = 0;
        }

        victim.Health.ReduceBy(damage);

        if (victim.entityType == EntityType.Type.Player)
        {
            healthBarController.SetHealth(victim.Health.Get());
        }
    }
}