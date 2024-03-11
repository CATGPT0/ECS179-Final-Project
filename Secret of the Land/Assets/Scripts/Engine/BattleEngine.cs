using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;


public enum AttackType
{
    Physical,
    Magical
}

    
public class BattleEngine : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public void DealDamage<TAttacker, TVictim>(ref TAttacker attacker, ref TVictim victim) where TAttacker : Properties where TVictim : Properties
    {
        int attackPower = attacker.AttackPower;
        int damage = 0;
        var type = attacker.AttackType;
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
    }
}