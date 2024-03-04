using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedComponent : MonoBehaviour, IProperty
{
    [SerializeField]
    private int speed;
    public void ReduceBy(int amount)
    {
        speed -= amount;
    }

    public void IncreaseBy(int amount)
    {
        speed += amount;
    }

    public void SetTo(int amount)
    {
        speed = amount;
    }

    public int Get()
    {
        return speed;
    }
}