using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    protected override void Init()
    {
        base.Init();
        entityType = EntityType.Type.Player;
    }
    void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health: " + health);
        Debug.Log("maxHealth: " + maxHealth);
        Debug.Log("attackPower: " + attackPower);
        Debug.Log("armor: " + armor);
    }
}
