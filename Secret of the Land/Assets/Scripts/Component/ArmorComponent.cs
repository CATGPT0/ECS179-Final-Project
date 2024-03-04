using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorComponent : MonoBehaviour, IProperty
{
    [SerializeField]
    private int armor;
    public void ReduceBy(int amount)
    {
        armor -= amount;
    }

    public void IncreaseBy(int amount)
    {
        armor += amount;
    }

    public void SetTo(int amount)
    {
        armor = amount;
    }

    public int Get()
    {
        return armor;
    }
}
