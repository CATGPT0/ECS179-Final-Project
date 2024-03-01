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
        public static void DealDamage(EntityController attacker, EntityController victim, AttackType type)
        {
            int attackPower = attacker.AttackPower;
            int damage = 0;
            
            if (type == AttackType.Physical)
            {
                damage = attackPower - victim.Armor;
            }
            else if (type == AttackType.Magical)
            {
                damage = attackPower - victim.MagicResist;
            }
            victim.Health -= damage;
        }
    }
}

