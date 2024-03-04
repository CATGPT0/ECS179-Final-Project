using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProperty
{
    public void ReduceBy(int amount);
    public void IncreaseBy(int amount);
    public void SetTo(int amount);
    public int Get();
}
