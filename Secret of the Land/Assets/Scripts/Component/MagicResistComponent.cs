using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicResistComponent : MonoBehaviour, IProperty
{
    [SerializeField]
    private int magicResist;
    public void ReduceBy(int amount)
    {
        magicResist -= amount;
    }

    public void IncreaseBy(int amount)
    {
        magicResist += amount;
    }

    public void SetTo(int amount)
    {
        magicResist = amount;
    }

    public int Get()
    {
        return magicResist;
    }
}
