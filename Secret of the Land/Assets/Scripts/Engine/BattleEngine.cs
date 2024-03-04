using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Engine
{
    public enum AttackType
    {
        Physical,
        Magical
    }
    public static class BattleEngine
    {
        public static void DealDamage(Entity attacker, Entity victim, AttackType type)
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
            Debug.Log("armor: " + victim.Armor.Get());
            Debug.Log("Attack Power: " + attackPower);
            Debug.Log("Damage: " + damage);
            victim.Health.ReduceBy(damage);
            Debug.Log(victim.Health.Get());
        }
    }
}

