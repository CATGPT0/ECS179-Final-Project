using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IProperty
{
    
    [SerializeField]
    private int health;

    public void ReduceBy(int amount)
    {
        health -= amount;
    }
    public void IncreaseBy(int amount)
    {
        health += amount;

    }
    public void SetTo(int amount)
    {
        health = amount;
    }
    public int Get()
    {
        return health;
    }
}