using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerComponent : MonoBehaviour, IProperty
{   
    [SerializeField]
    private int attackPower;
    public void ReduceBy(int amount)
    {
        attackPower -= amount;
    }

    public void IncreaseBy(int amount)
    {
        attackPower += amount;
    }

    public void SetTo(int amount)
    {
        attackPower = amount;
    }

    public int Get()
    {
        return attackPower;
    }
}

