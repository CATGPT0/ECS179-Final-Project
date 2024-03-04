using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private XPComponent xpComponent;
    [SerializeField]
    private LevelComponent levelComponent;
    private Entity entity;

    void Awake()
    {
        xpComponent = GetComponentInChildren<XPComponent>();
        levelComponent = GetComponentInChildren<LevelComponent>();
        entity = FindFirstObjectByType<Entity>();
    }

    public void GetXP(int amount)
    {
        xpComponent.IncreaseBy(amount);
        if (xpComponent.Get() >= 100)
        {
            IncreaseLevel(1);
            xpComponent.SetTo(0);
            entity.AttackPower.IncreaseBy(2);
        }
        Debug.Log("XP: " + xpComponent.Get());
        Debug.Log("Level: " + levelComponent.Get());
    }

    public void IncreaseLevel(int amount)
    {
        levelComponent.IncreaseBy(amount);
    }
}
