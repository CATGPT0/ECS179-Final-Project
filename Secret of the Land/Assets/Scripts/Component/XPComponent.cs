using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPComponent : MonoBehaviour, IProperty
{
    [SerializeField]
    private int xp;
    public void ReduceBy(int amount)
    {
        xp -= amount;
    }

    public void IncreaseBy(int amount)
    {
        xp += amount;
    }

    public void SetTo(int amount)
    {
        xp = amount;
    }

    public int Get()
    {
        return xp;
    }
}
