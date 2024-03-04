using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour, IProperty
{
    [SerializeField]
    private int level;
    public void ReduceBy(int amount)
    {
        level -= amount;
    }

    public void IncreaseBy(int amount)
    {
        level += amount;
    }

    public void SetTo(int amount)
    {
        level = amount;
    }

    public int Get()
    {
        return level;
    }
}

